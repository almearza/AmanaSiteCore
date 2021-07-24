using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class VideoViewComponent:ViewComponent
    {
         private readonly IUnitOfWork _unitOfWork;
        public VideoViewComponent(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var video =await _unitOfWork.Video.GetLatestVideoAsync();
            return View(video);
        }
       
    }
}