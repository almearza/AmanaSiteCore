using System.Threading.Tasks;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class NewsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var top5news =await _unitOfWork.News.GetTop5NewsAsync();
            return View(top5news);
        }

    }
}