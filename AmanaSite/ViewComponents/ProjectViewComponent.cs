using System.Threading.Tasks;
using AmanaSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.ViewComponents
{
    public class ProjectViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var _projects = await _unitOfWork.Project.GetLatest6ProjectsAsync();
            return View(_projects);
        }

    }
}