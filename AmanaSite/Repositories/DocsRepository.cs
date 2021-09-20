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
    public class DocsRepository : Idocs
    {
        private readonly DContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICurrentLang _currentLang;
        public DocsRepository(DContext context, IWebHostEnvironment env, ICurrentLang currentLang)
        {
            this._currentLang = currentLang;
            this._env = env;
            this._context = context;

        }
        public async Task DeleteDoc(int id)
        {
            var _doc = await _context.AmanaDocs.FindAsync(id);
            if (_doc == null) return;

            var img_ServerPath = Path.Combine(_env.ContentRootPath, "wwwroot", "pdfs");
            var pathAndName = Path.Combine(img_ServerPath, _doc.Link);
            File.Delete(pathAndName);
            _context.AmanaDocs.Remove(_doc);
        }

        public async Task<AmanaDocs> AddDocAsync(AmanaDocs model, IFormFile file)
        {
            if (file != null)
            {
                var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
                var imgTitle = "pdf_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
                var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "pdfs");
                if (!Directory.Exists(img_ServerSavePath))
                {
                    Directory.CreateDirectory(img_ServerSavePath);
                }
                var imgPathWithName = Path.Combine(img_ServerSavePath, imgTitle);
                using (Stream fileStream = new FileStream(imgPathWithName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.Link = imgTitle;
            }

            model.ModifiedDate = DateTime.Now;
            await _context.AmanaDocs.AddAsync(model);
            return model;
        }

        public async Task<PagingResponse<AmanaDocs>> GetPaggedDocsAsync(PagingRequest pagingRequest)
        {
             var query = _context.AmanaDocs.OrderByDescending(n => n.ModifiedDate).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_doc => _doc.Name.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<AmanaDocs>
            .GetPaggedList(pagingRequest, query.AsNoTracking());
        }
        public async Task<IEnumerable<AmanaDocs>> GetDocsAsync(){
            return await _context.AmanaDocs
            .Where(m=>m.LangCode==_currentLang.Get())
            .OrderByDescending(m=>m.ModifiedDate)
            .ToListAsync();
        }
    }
}