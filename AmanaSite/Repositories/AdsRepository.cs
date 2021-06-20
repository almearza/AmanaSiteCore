using System;
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
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class AdsRepository : IAds
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdsRepository(DContext context, IMapper mapper, IWebHostEnvironment env)
        {
            this._env = env;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task HandleAdssAsync(SlidersShow model, IFormFileCollection files)
        {
            
            if (files.Count > 0 && files[0] != null)
            {
                var file = files[0];
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "ads");
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

            model.UploadDate = DateTime.Now;
            model.Active = true;
            if (model.Id == 0) await _context.SlidersShows.AddAsync(model);
        }
        public async Task Activate(int id)
        {
            var ads = await _context.SlidersShows.FindAsync(id);
            ads.Active = !ads.Active;
        }

        public async Task<PagingResponse<SlidersShowVM>> GetAdsAsync(PagingRequest pagingRequest)
        {
            var query = _context.SlidersShows.OrderByDescending(n => n.UploadDate).AsQueryable();
            
            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_ads => _ads.Title.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<SlidersShowVM>
            .GetPaggedList(pagingRequest, query.ProjectTo<SlidersShowVM>(_mapper.ConfigurationProvider).AsNoTracking());
        }

        public async Task<SlidersShow> GetAdsByIdAsync(int id)
        {
            return await _context.SlidersShows.FindAsync(id);
        }


    }
}