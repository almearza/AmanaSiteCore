using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface Idocs
    {
        Task<AmanaDocs> AddDocAsync(AmanaDocs model, IFormFile file);
        Task DeleteDoc(int id);
        Task<PagingResponse<AmanaDocs>> GetPaggedDocsAsync(PagingRequest pagingRequest);
    }
}