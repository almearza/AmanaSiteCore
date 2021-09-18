using System;
using System.Collections.Generic;
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
    public class InfoRepository : IInfo
    {
        private readonly DContext _context;
        private readonly ICurrentLang _currentLang;
        private readonly IWebHostEnvironment _env;
        public InfoRepository(DContext context, IWebHostEnvironment env, ICurrentLang currentLang)
        {
            this._env = env;
            this._currentLang = currentLang;
            this._context = context;

        }
        public async Task<Info> AddInfoAsync(Info model, IFormFile file)
        {
            if (file != null)
            {
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "info");
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
                model.ModifiedDate = DateTime.Now;
                await _context.Info.AddAsync(model);
            }
            return model;
        }

        public async Task DeleteInfo(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info == null) return;

            var img_ServerPath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "info");
            var pathAndName = Path.Combine(img_ServerPath, info.ImgUrl);
            File.Delete(pathAndName);
            _context.Info.Remove(info);
        }

        public async Task<PagingResponse<Info>> GetPaggedInfoAsync(PagingRequest pagingRequest)
        {
             var query = _context.Info.OrderByDescending(n => n.ModifiedDate).AsQueryable();
            return await PagingResponse<Info>
            .GetPaggedList(pagingRequest, query.AsNoTracking());
        }
        public async Task<IEnumerable<Info>> GetLatest6InfoAsync()
        {
            var _info = await _context.Info
            .Where(v=>v.LangCode == _currentLang.Get())
            .OrderBy(v => v.ModifiedDate)
            .Take(6)
            .ToListAsync();
            return _info;
        }
    }
}