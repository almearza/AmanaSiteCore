using System.Threading.Tasks;
using AmanaSite.Remote;
using AutoMapper;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AmanaSite.Controllers
{
    public class InvestmentCenterController : Controller
    {
        private readonly AmanaApi _amanaApi;
        private readonly IStringLocalizer<ContactusController> _localizer;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IMapper _mapper;
        public InvestmentCenterController(AmanaApi amanaApi, IMapper mapper, IStringLocalizer<ContactusController> localizer, ICaptchaValidator captchaValidator)
        {
            _localizer = localizer;
            this._captchaValidator = captchaValidator;
            _amanaApi = amanaApi;

            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactInvest(ContactMunVM model, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("", _localizer["InValidCaptcha"]);
                return View("Index", model);

            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            if (string.IsNullOrEmpty(model.contactReason))
            {
                ModelState.AddModelError("", _localizer["ContactReasonRequired"]);
                return View("Index", model);
            }
            var _contactModel = _mapper.Map<ContactMun>(model);
            var result = await _amanaApi.ContactInvest(_contactModel);
            if (!result)
            {
                ModelState.AddModelError("", _localizer["SomeError"]);
                return View("Index", model);
            }
            return RedirectToAction("Success", "Home");
        }
    }
}