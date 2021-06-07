using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.VM;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using AmanaSite.Extensions;
using AmanaSite.Helpers;

namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy = "NewsLevel")]
    public class NewsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("create-news")]
        public async Task<ActionResult> CreateNews(IFormCollection model)
        {
            if (model == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            var _files = HttpContext.Request.Form.Files;
            var files = model.Files;
            if (files == null || files.Count == 0) return BadRequest("الرجاء ارفاق صورة الخبر");

            var news = new New();
            news.Title = model["title"].ToString();
            news.Descr = model["descr"].ToString();
            var typeIdStr = model["typeId"].ToString();
            news.TypeId = Convert.ToInt32(typeIdStr);
            news.LangCode = (LangCode)Convert.ToInt32(model["langCode"].ToString());
            news.NewsResource = model["newsResource"].ToString();

            news.UploadedBy = User.GetUsername();

            if (string.IsNullOrEmpty(news.Title) || string.IsNullOrEmpty(news.Descr) || news.LangCode == 0 || string.IsNullOrEmpty(news.NewsResource))
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");


            await _unitOfWork.News.CreateNewsAsync(news, files);
            var result = await _unitOfWork.Complete();
            if (result) return Created("", news);
            return BadRequest("حدث خطأ أثناء انشاء الخبر");
        }
        [HttpGet("get-types")]
        public async Task<IEnumerable<NewsType>> GetNewsTypes()
        {
            return await _unitOfWork.News.GetTypesAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsVM>>> GetNews([FromQuery] PaginationParams paginationParams)
        {
            var news = await _unitOfWork.News.GetNewsAsync(paginationParams);
            Response.AddPaginationHeader(news.CurrentPage, news.TotalPages, news.TotalCount, news.PageSize);
            return Ok(news);
        }
    }
}