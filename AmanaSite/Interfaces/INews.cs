using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers;
using AmanaSite.Models;
using AmanaSite.Models.VM;

namespace AmanaSite.Interfaces
{
    public interface INews
    {
        Task CreateNewsAsync(NewsVM model);
        Task<NewsVM> GetNewsByIdAsync(int id);
        Task EditNewsAsync(int id,NewsVM model);
        Task Activate(int id);
        Task<IEnumerable<NewsType>> GetTypesAsync();
        Task<Task<PageList<NewsVM>>> GetNewsAsync();   
    }
}