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
        public async Task<JsonResult> Index(SurveyVM model)
        {
            if (!ModelState.IsValid)
            {
                var _capchaErrors = ModelState.Values.Where(m => m.ValidationState == ModelValidationState.Invalid && m.Errors.Any(m => m.ErrorMessage == "InValidCaptcha")).ToList();
                if (_capchaErrors != null)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new string[] { _localizer["InValidCaptcha"] }
                    });
                }
                var _errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                                .Select(m => m.ErrorMessage).ToArray();
                return Json(new
                {
                    success = false,
                    errors = _errors
                });
            }
            _unitOfWork.Survey.CreateSurvey(model);
            var result = await _unitOfWork.Complete();
            if (!result)
            {
                return Json(new
                {
                    success = false,
                    errors = new string[] { _localizer["SomeError"] }
                });
            }
            return Json(new
            {
                success = true
            });
        }
    }
}