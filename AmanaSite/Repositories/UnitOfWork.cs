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
        private readonly ICurrentLang _currentLang;

        public UnitOfWork(DContext context,
                          IMapper mapper,
                          IWebHostEnvironment evn,
                          ICurrentLang currentLang)
        {
            this._evn = evn;
            this._currentLang = currentLang;
            this._mapper = mapper;
            this._context = context;

        }

        public INews News => new NewsRepository(_context, _mapper, _evn,_currentLang);

        public IAds Ads => new AdsRepository(_context, _mapper, _evn, _currentLang);

        // public IAmanaService AmanaService => new AmanaServiceRepository(_context,_mapper,_evn);

        // public IMob Mob => new MobRepository(_context,_mapper,_evn);

        public IVideo Video => new VideoRepository(_context, _evn,_currentLang);
        public IProject Project => new ProjectRepository(_context, _evn,_mapper,_currentLang);
        public IBaladyat Baladyat => new BaladyatRepository(_context, _mapper, _evn, _currentLang);
        public IInfo Info => new InfoRepository(_context, _evn, _currentLang);

        public Idocs Docs => new DocsRepository(_context, _evn, _currentLang);

        public ISurvey Survey => new SurveyRepository(_context,_mapper,_currentLang);

        public async Task<bool> Complete()
        {
            // var has_change=HasChanges();
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}