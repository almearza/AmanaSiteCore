using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface INews
    {
        Task CreateNewsAsync(NewsVM model, IFormFileCollection files);
        Task UpdateNewsAsync(NewsVM newsVM,New news, IFormFileCollection files);
        Task<New> GetNewsByIdAsync(int id);
        Task Activate(int id);
        Task<IEnumerable<NewsType>> GetTypesAsync();
        Task<PagingResponse<NewsVM>> GetNewsAsync(PagingRequest pagingRequest);   
    }
}