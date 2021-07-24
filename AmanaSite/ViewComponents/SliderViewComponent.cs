using System.Threading.Tasks;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
         private readonly IUnitOfWork _unitOfWork;
        public SliderViewComponent(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ads = await _unitOfWork.Ads.GetTop5AdsAsync();
            return View(ads);
        }
       
    }
}