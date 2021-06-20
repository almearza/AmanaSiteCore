using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.VM;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AmanaSite.Extensions;
using AmanaSite.Helpers.DataTables;
using AutoMapper;

namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy = "NewsLevel")]
    public class NewsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpPost("handle-news")]
        public async Task<ActionResult> HandleNews([FromForm] NewsVM newsVM)
        {
            // IFormCollection model;
            if (newsVM == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            var newsId = newsVM.Id;

            var _files = HttpContext.Request.Form.Files;
            // var files = model.Files;
            if ((_files == null || _files.Count == 0) && newsId == 0) return BadRequest("الرجاء ارفاق صورة الخبر");

            newsVM.UploadedBy = User.GetUsername();

            if (newsVM.Id == 0)
                await _unitOfWork.News.CreateNewsAsync(newsVM, _files);
            else
            {
                var news = await this._unitOfWork.News.GetNewsByIdAsync(newsVM.Id);
                if (news == null)
                    return BadRequest("لا يوجد خبر بهذا الرقم المرسل");
                await _unitOfWork.News.UpdateNewsAsync(newsVM, news, _files);
            }

            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء-تعديل الخبر");
        }
        [HttpGet("get-types")]
        public async Task<IEnumerable<NewsType>> GetNewsTypes()
        {
            return await _unitOfWork.News.GetTypesAsync();
        }
        [HttpPost("get-pagged-news")]
        public async Task<ActionResult<IEnumerable<NewsVM>>> GetNews([FromBody] PagingRequest pagingRequest)
        {
            var news = await _unitOfWork.News.GetNewsAsync(pagingRequest);
            return Ok(news);
        }
        [HttpGet("get-news/{id}")]
        public async Task<ActionResult<NewsVM>> GetNews(int id)
        {
            var news = await _unitOfWork.News.GetNewsByIdAsync(id);
            return Ok(news);
        }
        [HttpPut("activate-news/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.News.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل الخبر");
        }

    }
}