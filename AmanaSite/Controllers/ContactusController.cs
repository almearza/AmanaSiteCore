using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Remote;
// using AspNetCore.ReCaptcha;
using AutoMapper;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace AmanaSite.Controllers
{
    public class ContactusController : Controller
    {
        private readonly AmanaApi _amanaApi;
        private readonly IStringLocalizer<ContactusController> _localizer;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IMapper _mapper;
        public ContactusController(AmanaApi amanaApi, IMapper mapper, IStringLocalizer<ContactusController> localizer, ICaptchaValidator captchaValidator)
        {
            _localizer = localizer;
            this._captchaValidator = captchaValidator;
            _amanaApi = amanaApi;

            _mapper = mapper;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactAmana(ContactMunVM model, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("", _localizer["InValidCaptcha"]);
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                var _errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                                .Select(m => m.ErrorMessage).ToArray();
                return Json(new
                {
                    success = false,
                    errors = _errors
                });
            }
            if (string.IsNullOrEmpty(model.contactReason))
            {
                return Json(new
                {
                    success = false,
                    errors = new string[] { _localizer["ContactReasonRequired"] }
                });
            }
            var _contactModel = _mapper.Map<ContactMun>(model);
            var result = await _amanaApi.ContactMun(_contactModel);
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
        public IActionResult ContactMayor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactMayor(ContactMunVM model, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("", _localizer["InValidCaptcha"]);
                return View(model);

            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (string.IsNullOrEmpty(model.contactReason))
            {
                ModelState.AddModelError("", _localizer["ContactReasonRequired"]);
                return View(model);
            }
            var _contactModel = _mapper.Map<ContactMun>(model);
            var result = await _amanaApi.ContactAmeen(_contactModel);
            if (!result)
            {
                ModelState.AddModelError("", _localizer["SomeError"]);
                return View(model);
            }
            return RedirectToAction("Success", "Home");
        }
    }
}