using System.Threading.Tasks;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class ProjectViewComponent:ViewComponent
    {
        public ProjectViewComponent()
        {
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
       
    }
}