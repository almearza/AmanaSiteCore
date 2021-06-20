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
using System;

namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy = "AdsLevel")]
    public class AdsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("handle-ads")]
        public async Task<ActionResult> HandleAds([FromForm] SlidersShowVM sliderShowVM)
        {
            // IFormCollection model;
            if (sliderShowVM == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (sliderShowVM.CanExpire && sliderShowVM.ExpireDate == null)
                return BadRequest("الرجاء التأكد من إدخال تاريخ انتهاء الاعلان");
            if (sliderShowVM.ExpireDate != null && sliderShowVM.ExpireDate <= DateTime.Now)
                return BadRequest("عفوا تاريخ انتهاء الاعلان يجب ان يكون اكبر من تاريخ اليوم");

            var sliderId = sliderShowVM.Id;

            var _files = HttpContext.Request.Form.Files;
            // var files = model.Files;
            if ((_files == null || _files.Count == 0) && sliderId == 0) return BadRequest("الرجاء ارفاق صورة الإعلان");
            var slider = new SlidersShow();
            if (sliderId != 0)
            {
                slider = await _unitOfWork.Ads.GetAdsByIdAsync(sliderId);
                sliderShowVM.ImgUrl = slider.ImgUrl;
            }
            _mapper.Map(sliderShowVM, slider);
            slider.UploadedBy = User.GetUsername();

            await _unitOfWork.Ads.HandleAdssAsync(slider, _files);
            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء الإعلان");
        }
        [HttpPost("get-pagged-ads")]
        public async Task<ActionResult<IEnumerable<SlidersShowVM>>> GetAds([FromBody] PagingRequest pagingRequest)
        {
            var ads = await _unitOfWork.Ads.GetAdsAsync(pagingRequest);
            return Ok(ads);
        }
        [HttpGet("get-ads/{id}")]
        public async Task<ActionResult<SlidersShowVM>> GetAds(int id)
        {
            var ads = await _unitOfWork.Ads.GetAdsByIdAsync(id);
            return Ok(ads);
        }
        [HttpPut("activate-ads/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.Ads.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل الإعلان");
        }

    }
}