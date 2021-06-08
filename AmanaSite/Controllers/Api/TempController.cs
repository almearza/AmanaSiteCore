using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Helpers;
using System.Linq;
using AmanaSite.Models;
using AmanaSite.Data;
using AmanaSite.Models.VM;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Controllers.Api
{
    public class TempController : ApiBaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly DContext _context;
        private readonly IMapper _mapper;
        public TempController(IWebHostEnvironment env, DContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            this._env = env;

        }
        [HttpPost]
        public async Task<ActionResult> GetUsers([FromBody] PagingRequest paging)
        {
            var requestBody = Request.Body;
            var pagingResponse = new PagingResponse<NewsVM>()
            {
                Draw = paging.draw
            };


            IQueryable<New> query = null;

            if (!string.IsNullOrEmpty(paging.search.value))
            {
                query = _context.News.Where(_new => _new.Title.Contains(paging.search.value));
            }
            else
            {
                query = _context.News;
            }

            var recordsTotal =await _context.News.CountAsync();
            

            var colOrder = paging.order[0];

            switch (colOrder.column)
            {
                case 0:
                    query = colOrder.dir == "asc" ? query.OrderBy(_new => _new.Id) : query.OrderByDescending(_new => _new.Id);
                    break;
                case 1:
                    query = colOrder.dir == "asc" ? query.OrderBy(_new => _new.Title) : query.OrderByDescending(_new => _new.Title);
                    break;
                case 2:
                    query = colOrder.dir == "asc" ? query.OrderBy(_new => _new.Descr) : query.OrderByDescending(_new => _new.Descr);
                    break;
                case 4:
                    query = colOrder.dir == "asc" ? query.OrderBy(_new => _new.NewsDate) : query.OrderByDescending(_new => _new.NewsDate);
                    break;
            }
            
            var recordsFiltered = query.Count();

            pagingResponse.Data = await query.ProjectTo<NewsVM>(_mapper.ConfigurationProvider).Skip(paging.start).Take(paging.length).ToListAsync();
            pagingResponse.RecordsTotal = recordsTotal;
            pagingResponse.RecordsFiltered =  recordsTotal;


            return Ok(pagingResponse);
        }
    }

}