using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
     public class InfoViewComponent:ViewComponent
    {
        public InfoViewComponent()
        {
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
       
    }
}