using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;

namespace AmanaSite.Interfaces
{
    public interface IBaladyat
    {
        Task HandleBaladyaAsync(Baladyat model);
        Task<Baladyat> GetBaladyaByIdAsync(int id);
        Task Activate(int id);
        Task<PagingResponse<Baladyat>> GetBaladyatAsync(PagingRequest pagingRequest);
        Task <IEnumerable<Baladyat>> GetBaladyatByTypeAsync(BaladyaType type);
    }
}