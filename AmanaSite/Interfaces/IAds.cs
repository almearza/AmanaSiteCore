using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface IAds
    {
        Task HandleAdssAsync(SlidersShow model, IFormFileCollection files);
        Task<SlidersShow> GetAdsByIdAsync(int id);
        Task Activate(int id);
        Task<PagingResponse<SlidersShowVM>> GetAdsAsync(PagingRequest pagingRequest);
        Task <IEnumerable<SlidersShowVM>> GetTop5AdsAsync();
    }
}