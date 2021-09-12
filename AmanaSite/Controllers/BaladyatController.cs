using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers
{
    public class BaladyatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaladyatController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int id)
        {
            var baladyat = await _unitOfWork.Baladyat.GetBaladyatByTypeAsync((BaladyaType)id);
            return View(baladyat);
        }
    }
}