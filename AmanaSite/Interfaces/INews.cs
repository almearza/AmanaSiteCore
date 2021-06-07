using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface INews
    {
        Task CreateNewsAsync(New model, IFormFileCollection files);
        Task<New> GetNewsByIdAsync(int id);
        Task EditNewsAsync(int id,New model);
        Task Activate(int id);
        Task<IEnumerable<NewsType>> GetTypesAsync();
        Task<PageList<NewsVM>> GetNewsAsync(PaginationParams paginationParams);   
    }
}