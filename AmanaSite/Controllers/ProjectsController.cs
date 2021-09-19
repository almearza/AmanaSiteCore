using System;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers
{
    public class ProjectsController :Controller
    {
         private readonly IUnitOfWork _unitOfWork;
        public ProjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> Index(int pageIndex)
        {
            var pageSize=6;
            if (pageSize <= 0 ||pageIndex<0) return BadRequest("not allowed size");
            var projectsList = await _unitOfWork.Project.GetProjectsByIndexAsync(pageIndex,pageSize);
            var _totalOfNews = await _unitOfWork.Project.GetTotalAsync();
            var _round=Math.Ceiling((double)_totalOfNews/pageSize);
            var _pagination = new ProjectsPagination
            {
                Projects = projectsList,
                PageTotal=(int)_round,
                ActivePageIndex=pageIndex+1
            };
            return View(_pagination);
        }

    }
}