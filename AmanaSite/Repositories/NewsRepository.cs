using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers;
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
    public class NewsRepository : INews
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ICurrentLang _currentLang;

        public NewsRepository(DContext context, IMapper mapper, IWebHostEnvironment env, ICurrentLang currentLang)
        {
            _currentLang = currentLang;
            this._env = env;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task Activate(int id)
        {
            var news = await _context.News.FindAsync(id);
            news.Active = !news.Active;
        }

        public async Task CreateNewsAsync(NewsVM model, IFormFileCollection files)
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

            var news = _mapper.Map<New>(model);
            news.Type = null;
            await _context.News.AddAsync(news);
        }

        public async Task<PagingResponse<NewsVM>> GetNewsAsync(PagingRequest pagingRequest)
        {
            var query = _context.News.OrderByDescending(n => n.NewsDate).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_new => _new.Title.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<NewsVM>
            .GetPaggedList(pagingRequest, query.ProjectTo<NewsVM>(_mapper.ConfigurationProvider).AsNoTracking());
        }

        public async Task<New> GetNewsByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<IEnumerable<NewsVM>> GetNewsByIndexAsync(int pageIndex, int pageSize)
        {
            var news = await _context.News
            .OrderByDescending(n => n.NewsDate)
            .Where(m => m.LangCode ==_currentLang.Get()&&m.Active)
            .Skip((pageIndex*pageSize))
            .ProjectTo<NewsVM>(_mapper.ConfigurationProvider)
            .Take(pageSize)
            .ToListAsync();
            return news;
        }

        public async Task<IEnumerable<NewsVM>> GetTop5NewsAsync()
        {
            //2:ksa , 3:Madina , 4:Amana
            var topThreeNews = new List<NewsVM>();
            for (var i = 2; i < 5; i++)
            {
                var top = await _context.News.ProjectTo<NewsVM>(_mapper.ConfigurationProvider)
                .OrderByDescending(n => n.NewsDate)
                .Where(n => n.Active && n.LangCode == _currentLang.Get() && n.TypeId == i)
                .FirstOrDefaultAsync();
                if (top != null)
                    topThreeNews.Add(top);
            }
            return topThreeNews;
        }

        public async Task<int> GetTotalAsync()
        {
            return await _context.News.CountAsync();
        }

        public async Task<IEnumerable<NewsType>> GetTypesAsync()
        {
            return await _context.NewsTypes.ToListAsync();
        }

        public async Task UpdateNewsAsync(NewsVM newsVM, New news, IFormFileCollection files)
        {
            news.Title = newsVM.Title;
            news.Descr = newsVM.Descr;
            news.TypeId = newsVM.TypeId;
            news.NewsResource = newsVM.NewsResource;
            news.LangCode = newsVM.LangCode;
            news.UploadedBy = newsVM.UploadedBy;
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
                news.ImgUrl = imgTitle;
            }
            _context.Entry(news).State = EntityState.Modified;

        }
    }
}