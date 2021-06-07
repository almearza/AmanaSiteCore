using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers;
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
    public class NewsRepository : INews
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public NewsRepository(DContext context, IMapper mapper, IWebHostEnvironment env)
        {
            this._env = env;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task Activate(int id)
        {
            var news = await _context.News.FindAsync(id);
            news.Active = !news.Active;
        }

        public async Task CreateNewsAsync(New model, IFormFileCollection files)
        {
            // foreach (var file in files)
            // {

            if (files.Count > 0 && files[0] != null)
            {
                var file = files[0];
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "news");
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
            // }

            model.NewsDate = DateTime.Now;
            model.Active = true;
            await _context.News.AddAsync(model);
        }

        public Task EditNewsAsync(int id, New model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PageList<NewsVM>> GetNewsAsync(PaginationParams paginationParams)
        {
            var query = _context.News.AsQueryable();
            query.OrderByDescending(n=>n.NewsDate);
            
            return await PageList<NewsVM>.CreateAsync(query
            .ProjectTo<NewsVM>(_mapper.ConfigurationProvider).AsNoTracking()
            , paginationParams.PageNumber, paginationParams.PageSize);
        }

        public Task<New> GetNewsByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<NewsType>> GetTypesAsync()
        {
            return await _context.NewsTypes.ToListAsync();
        }
    }
}