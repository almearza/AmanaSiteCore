using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class ProjectRepository : IProject
    {
        private readonly DContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ICurrentLang _currentLang;

        public ProjectRepository(DContext context, IWebHostEnvironment env, IMapper mapper, ICurrentLang currentLang)
        {
            this._currentLang = currentLang;
            this._mapper = mapper;
            this._env = env;
            this._context = context;
        }
        public async Task Activate(int id)
        {
            var Project = await _context.Projects.FindAsync(id);
            Project.Active = !Project.Active;
        }

        public async Task<IEnumerable<Project>> GetLatest6ProjectsAsync()
        {
            var projects = await _context.Projects
            .Where(v => v.Active && v.LangCode == _currentLang.Get())
            .OrderBy(v => v.ModifiedDate)
            .Take(6)
            .ToListAsync();
            return projects;
        }

        public async Task<Project> GetProjectByIdAsync(int Id)
        {
            var Project = await _context.Projects.FindAsync(Id);
            return Project;
        }

        public async Task<PagingResponse<Project>> GetProjectsAsync(PagingRequest pagingRequest)
        {
            var query = _context.Projects.OrderByDescending(n => n.ModifiedDate).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_Project => _Project.Title.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<Project>
            .GetPaggedList(pagingRequest, query.AsNoTracking());
        }

        public async Task HandleProjectAsyn(Project model, IFormFile img)
        {
            var _pro = new Project();
            if (model.Id != 0)
            {
                _pro = await _context.Projects.FindAsync(model.Id);
                model.ImgUrl = _pro.ImgUrl;
            }
            _mapper.Map(model, _pro);
            if (img != null)
            {
                var imgExt = img.FileName.Substring(img.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "projects");
                if (!Directory.Exists(img_ServerSavePath))
                {
                    Directory.CreateDirectory(img_ServerSavePath);
                }
                var imgPathWithName = Path.Combine(img_ServerSavePath, imgTitle);
                using (Stream fileStream = new FileStream(imgPathWithName, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                _pro.ImgUrl = imgTitle;
            }

            _pro.ModifiedDate = DateTime.Now;
            _pro.Active = true;
            if (_pro.Id == 0) await _context.Projects.AddAsync(_pro);
        }
    }
}