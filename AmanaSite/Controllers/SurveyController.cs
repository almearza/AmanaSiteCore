using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.Survey;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace AmanaSite.Controllers
{

    public class SurveyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SurveyController> _localizer;
        public SurveyController(IUnitOfWork unitOfWork, IStringLocalizer<SurveyController> localizer)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }
        public IActionResult Index(string lang)
        {
            return View();
        }
        [HttpPost]
        [ValidateReCaptcha(ErrorMessage = "InValidCaptcha")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SurveyVM model)
        {
            if (!ModelState.IsValid)
            {
                var _errors = ModelState.Values.Where(m => m.ValidationState == ModelValidationState.Invalid && m.Errors.Any(m => m.ErrorMessage == "InValidCaptcha")).ToList();
                if (_errors!=null)
                {
                    ModelState.AddModelError("", _localizer["InValidCaptcha"]);
                }
                return View(model);
            }
            _unitOfWork.Survey.CreateSurvey(model);
            var result = await _unitOfWork.Complete();
            if (!result)
            {
                ModelState.AddModelError("", _localizer["SomeError"]);
                return View(model);
            }
            return RedirectToAction("sucess", "home");
        }
    }
}