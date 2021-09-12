using System.Threading.Tasks;
using AmanaSite.Remote;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class CountersViewComponent : ViewComponent
    {
        private readonly AmanaApi _amanaApi;
        public CountersViewComponent(AmanaApi amanaApi)
        {
            _amanaApi = amanaApi;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var counter =await _amanaApi.GetCounterAsync();
            var _done =counter.projectsDone;
            return View(counter);
        }

    }
}