using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Interfaces;
using AutoMapper;

namespace AmanaSite.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;

        }
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