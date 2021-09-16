using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.Survey;
using GoogleReCaptcha.V3.Interface;
// using AspNetCore.ReCaptcha;
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
        private readonly ICaptchaValidator _captchaValidator;
        public SurveyController(IUnitOfWork unitOfWork, IStringLocalizer<SurveyController> localizer, ICaptchaValidator captchaValidator)
        {
            this._captchaValidator = captchaValidator;
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }
        public IActionResult Index(string lang)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SurveyVM model, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("",_localizer["InValidCaptcha"]);
                return View(model);
               
            }

            if (!ModelState.IsValid)
            {
               return View(model);
            }
            if (model.SGenderId == 0 || model.SVisitAvgId == 0 || model.SEvalEmpId == 0 || model.STransTypeId == 0)
            {
                ModelState.AddModelError("",_localizer["RequiredFields"]);
                return View(model);
            }
            _unitOfWork.Survey.CreateSurvey(model);
            var result = await _unitOfWork.Complete();
            if (!result)
            {
                
                ModelState.AddModelError("",_localizer["SomeError"]);
                return View(model);
            }
            return RedirectToAction("Success","Home");
        }
    }
}