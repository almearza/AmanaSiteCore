using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.VM;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy ="NewsLevel")]
    public class NewsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("create-news")]
        public async Task<ActionResult> CreateNews(NewsVM model)
        {
            if (!ModelState.IsValid) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (model.NewsImg == null || model.NewsImg.Length == 0) return BadRequest("الرجاء ارفاق صورة الخبر");
            await _unitOfWork.News.CreateNewsAsync(model);
            var result = await _unitOfWork.Complete();
            if (result) return Created("", model);
            return BadRequest("حدث خطأ أثناء انشاء الخبر");
        }
        [HttpGet("get-types")]
        public async Task<IEnumerable<NewsType>> GetNewsTypes(){
           return await _unitOfWork.News.GetTypesAsync(); 
        }
    }
}