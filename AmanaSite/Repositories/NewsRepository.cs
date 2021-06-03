using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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

        public async Task CreateNewsAsync(NewsVM model)
        {
            var img = model.NewsImg;
            var imgExt = img.FileName.Substring(img.FileName.LastIndexOf(".")).Replace("\"", "");
            var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
            var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "images", "news", imgTitle);

            using (Stream fileStream = new FileStream(img_ServerSavePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }

            var news = _mapper.Map<New>(model);
            news.ImgUrl = imgTitle;
            await _context.News.AddAsync(news);
        }

        public Task EditNewsAsync(int id, NewsVM model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Task<PageList<NewsVM>>> GetNewsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<NewsVM> GetNewsByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<NewsType>> GetTypesAsync()
        {
            return await _context.NewsTypes.ToListAsync();
        }
    }
}