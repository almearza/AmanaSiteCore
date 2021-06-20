using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface IMob
    {
        Task HandleMobAsyn(MAndF model, IFormFileCollection files);
        Task<MAndF> GetMobByIdAsync(int id);
        Task Activate(int id);
        Task<PagingResponse<MAndFVM>> GetMobAsync(PagingRequest pagingRequest);
        Task<IEnumerable<MAndFType>> GetMobTypes();
    }
}