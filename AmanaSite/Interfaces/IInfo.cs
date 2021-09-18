using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface IInfo
    {
        Task<Info> AddInfoAsync(Info model, IFormFile img);
        Task DeleteInfo(int id);
        Task<PagingResponse<Info>> GetPaggedInfoAsync(PagingRequest pagingRequest);
        Task<IEnumerable<Info>> GetLatest6InfoAsync();
    }
}