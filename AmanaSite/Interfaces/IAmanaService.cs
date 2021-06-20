using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Interfaces
{
    public interface IAmanaService
    {
        Task HandleServiceAsync(AmanaService model, IFormFileCollection files);
        Task<AmanaService> GetServiceByIdAsync(int id);
        Task Activate(int id);
        Task<PagingResponse<AmanaServiceVM>> GetServicesAsync(PagingRequest pagingRequest);
        Task<IEnumerable<ServiceType>> GetTypesAsync();
    }
}