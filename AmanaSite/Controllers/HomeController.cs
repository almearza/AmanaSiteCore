using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AmanaSite.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using AmanaSite.Interfaces;

namespace AmanaSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index(string lang)
        {
            ViewBag.IsSend = false;
            return View();
        }

        public IActionResult Structure()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public IActionResult setlang(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        public IActionResult Success()
        {
            ViewBag.IsSend = true;
            return View("Index");
        }

        public IActionResult OpenData()
        {
            return View();
        }
        public async Task<IActionResult> Docs()
        {
            var _docs = await _unitOfWork.Docs.GetDocsAsync();
            return View(_docs);
        }


    }
}
