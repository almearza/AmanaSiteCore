using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Remote;
using AspNetCore.ReCaptcha;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace AmanaSite.Controllers
{
    public class ContactusController : Controller
    {
        private readonly AmanaApi _amanaApi;
        private readonly IStringLocalizer<ContactusController> _localizer;
        private readonly IMapper _mapper;
        public ContactusController(AmanaApi amanaApi, IMapper mapper, IStringLocalizer<ContactusController> localizer)
        {
            _localizer = localizer;
            _amanaApi = amanaApi;
            
            _mapper = mapper;
        }
        [HttpPost]
        [ValidateReCaptcha(ErrorMessage = "InValidCaptcha")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactAmana(ContactMunVM model)
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
    }
}