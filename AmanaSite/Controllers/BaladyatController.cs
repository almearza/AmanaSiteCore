using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AmanaSite.Controllers
{
    public class BaladyatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<BaladyatController> _localizer;

        public BaladyatController(IUnitOfWork unitOfWork, IStringLocalizer<BaladyatController> localizer)
        {
            this._localizer = localizer;
            this._unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(BaladyaType id)
        {
            var baladyat = await _unitOfWork.Baladyat.GetBaladyatByTypeAsync(id);
            ViewBag.Title = id switch
            {
                BaladyaType.Sub => _localizer["SubBaladyat"],
                BaladyaType.Provinces=>_localizer["ProvBaladyat"],
                _=>_localizer["SuburbanBaladyat"]
            };
            ViewBag.Indicator=NavType.Sub;
            return View(baladyat);
        }
    }
}