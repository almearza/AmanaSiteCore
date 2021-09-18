using System;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> Index(int pageIndex)
        {
            var pageSize=9;
            if (pageSize <= 0 ||pageIndex<0) return BadRequest("not allowed size");
            var newsList = await _unitOfWork.News.GetNewsByIndexAsync(pageIndex,pageSize);
            var _totalOfNews = await _unitOfWork.News.GetTotalAsync();
            var _round=Math.Ceiling((double)_totalOfNews/pageSize);
            var _pagination = new BlogPagination
            {
                News = newsList,
                PageTotal=(int)_round,
                ActivePageIndex=pageIndex+1
            };
            return View(_pagination);
        }
    }
}