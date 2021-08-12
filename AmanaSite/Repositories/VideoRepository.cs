using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class VideoRepository : IVideo
    {
        private readonly DContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICurrentLang _currentLang;

        public VideoRepository(DContext context, IWebHostEnvironment env, ICurrentLang currentLang)
        {
            this._currentLang = currentLang;
            this._env = env;
            this._context = context;
        }
        public async Task Activate(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            video.Active = !video.Active;
        }

        public async Task<Video> GetLatestVideoAsync()
        {
            var video = await _context.Videos
            .Where(v => v.Active && v.LangCode == _currentLang.Get())
            .OrderBy(v => v.UploadDate)
            .LastOrDefaultAsync();
            return video;
        }

        public async Task<Video> GetVideoByIdAsync(int Id)
        {
            var video = await _context.Videos.FindAsync(Id);
            return video;
        }

        public async Task<PagingResponse<Video>> GetVideosAsync(PagingRequest pagingRequest)
        {
            var query = _context.Videos.Select(m =>
           new Video
           {
               Id = m.Id,
               Title = m.Title,
               Descr = m.Descr,
               Active = m.Active,
               ImgUrl = m.ImgUrl,
               Lang = (m.LangCode == LangCode.Ar) ? "العربية" : "English",
               LangCode = m.LangCode,
               Link = m.Link,
               UploadDate = m.UploadDate,
               UploadedBy = m.UploadedBy
           }).OrderByDescending(n => n.UploadDate).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_video => _video.Title.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<Video>
            .GetPaggedList(pagingRequest, query.AsNoTracking());
        }

        public async Task HandleVideoAsyn(Video model, IFormFile screenShot)
        {
            if (screenShot != null)
            {
                var file = screenShot;
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "Videos");
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
            if (model.Id == 0) await _context.Videos.AddAsync(model);
        }
    }
}