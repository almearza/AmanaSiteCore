using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace AmanaSite.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        public UnitOfWork(DContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            this._evn = evn;
            this._mapper = mapper;
            this._context = context;

        }

        public INews News => new NewsRepository(_context, _mapper,_evn);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 1;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}