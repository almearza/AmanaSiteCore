using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AmanaSite.Repositories
{
    public class AmanaServiceRepository : IAmanaService
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AmanaServiceRepository(DContext context, IMapper mapper, IWebHostEnvironment env)
        {
            this._env = env;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task HandleServiceAsync(AmanaService model, IFormFileCollection files)
        {

            if (files.Count > 0 && files[0] != null)
            {
                var file = files[0];
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "services");
                if (!Directory.Exists(img_ServerSavePath))
                {
                    Directory.CreateDirectory(img_ServerSavePath);
                }
                var imgPathWithName = Path.Combine(img_ServerSavePath, imgTitle);
                using (Stream fileStream = new FileStream(imgPathWithName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.ImgUrl = imgTitle;

            }

            model.UploadedDate = DateTime.Now;
            model.Active = true;
            if (model.Id == 0) await _context.AmanaServices.AddAsync(model);
        }
        public async Task Activate(int id)
        {
            var services = await _context.AmanaServices.FindAsync(id);
            services.Active = !services.Active;
        }

        public async Task<PagingResponse<AmanaServiceVM>> GetServicesAsync(PagingRequest pagingRequest)
        {
            var query = _context.AmanaServices.OrderByDescending(n => n.UploadedDate).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_service => _service.Descr.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<AmanaServiceVM>
            .GetPaggedList(pagingRequest, query.ProjectTo<AmanaServiceVM>(_mapper.ConfigurationProvider).AsNoTracking());
        }

        public async Task<AmanaService> GetServiceByIdAsync(int id)
        {
            return await _context.AmanaServices.FindAsync(id);
        }

        public async Task<IEnumerable<ServiceType>> GetTypesAsync()
        {
            var _types = await _context.ServiceTypes.ToListAsync();
            return _types;
        }
    }
}