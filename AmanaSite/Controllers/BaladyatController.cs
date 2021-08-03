using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers
{
    public class BaladyatController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}