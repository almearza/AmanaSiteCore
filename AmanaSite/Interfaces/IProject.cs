using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface IProject
    {
        Task HandleProjectAsyn(Project model, IFormFile img);
        Task<IEnumerable<Project>> GetLatest6ProjectsAsync();
        Task Activate(int id);
        Task<PagingResponse<Project>> GetProjectsAsync(PagingRequest pagingRequest);
        Task<Project> GetProjectByIdAsync(int Id);
        Task<IEnumerable<Project>> GetProjectsByIndexAsync(int pageIndex, int pageSize);
        Task<int> GetTotalAsync();
    }
}