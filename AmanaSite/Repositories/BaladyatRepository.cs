using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class BaladyatRepository : IBaladyat
    {
        public readonly DContext _context;
        public readonly IMapper _mapper;
        public readonly IWebHostEnvironment _env;
        public readonly ICurrentLang _currentLang;

        public BaladyatRepository(DContext context,
                            IMapper mapper,
                            IWebHostEnvironment env,
                            ICurrentLang currentLang)
        {
            this._env = env;
            this._currentLang = currentLang;
            this._mapper = mapper;
            this._context = context;
        }
        public async Task Activate(int id)
        {
            var baladya = await _context.Baladyat.FindAsync(id);
            baladya.Active = !baladya.Active;
        }

        public async Task<PagingResponse<Baladyat>> GetBaladyatAsync(PagingRequest pagingRequest)
        {
            var query = _context.Baladyat.OrderBy(n => n.BaladyaType).AsQueryable();

            //searching
            if (!string.IsNullOrEmpty(pagingRequest.search.value))
            {
                query = query.Where(_baladya => _baladya.Name.Contains(pagingRequest.search.value));
            }
            //projecting and getting filtered list
            return await PagingResponse<Baladyat>
            .GetPaggedList(pagingRequest, query.AsNoTracking());
        }

        public async Task<Baladyat> GetBaladyaByIdAsync(int id)
        {
            return await _context.Baladyat.FindAsync(id);
        }

        public async Task<IEnumerable<Baladyat>> GetBaladyatByTypeAsync(BaladyaType type)
        {
            return await _context.Baladyat.Where(b => b.BaladyaType == type).ToListAsync();
        }

        public async Task HandleBaladyaAsync(Baladyat model)
        {
            model.ModifiedDate = DateTime.Now;
            model.Active = true;
            if (model.Id == 0)
            {
                await _context.Baladyat.AddAsync(model);
            }
        }
    }
}