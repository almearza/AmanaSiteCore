using AmanaSite.Data;
using AmanaSite.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace AmanaSite.Repositories
{
    public abstract class BaseRepository
    {
        public readonly DContext _context;
        public readonly IMapper _mapper;
        public readonly IWebHostEnvironment _env;
        public readonly ICurrentLang _currentLang;

        public BaseRepository()
        {

        }
        public BaseRepository(DContext context,
                            IMapper mapper,
                            IWebHostEnvironment env,
                            ICurrentLang currentLang)
        {
            this._env = env;
            this._currentLang = currentLang;
            this._mapper = mapper;
            this._context = context;
        }

    }
}