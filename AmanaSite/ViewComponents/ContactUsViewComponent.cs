using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class ContactUsViewComponent : ViewComponent
    {
        public ContactUsViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}