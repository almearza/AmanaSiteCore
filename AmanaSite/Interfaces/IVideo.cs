using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Models;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Interfaces
{
    public interface IVideo
    {
        Task HandleVideoAsyn(Video model, IFormFile screenShot);
        Task<Video> GetLatestVideoAsync();
        Task Activate(int id);
        Task<PagingResponse<Video>> GetVideosAsync(PagingRequest pagingRequest);
        Task<Video> GetVideoByIdAsync(int Id);
    }
}