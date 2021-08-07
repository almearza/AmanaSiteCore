using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class BaladyatRepository : BaseRepository, IBaladyat
    {
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
            if (model.Id == 0)
            {
                model.Active = true;
                await _context.Baladyat.AddAsync(model);
            }
        }
    }
}