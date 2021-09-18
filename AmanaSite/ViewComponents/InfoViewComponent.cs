using System.Threading.Tasks;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class InfoViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public InfoViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var _info = await _unitOfWork.Info.GetLatest6InfoAsync();
            return View(_info);
        }

    }
}