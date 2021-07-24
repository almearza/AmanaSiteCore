using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Extensions;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy = "NewsLevel")]
    public class VideoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public VideoController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("handle-video")]
        public async Task<ActionResult> Handlevideo([FromForm] Video video)
        {
            // IFormCollection model;
            if (video == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");

            var videoId = video.Id;

            var _files = HttpContext.Request.Form.Files;
            // var files = model.Files;
            if ((_files == null || _files.Count == 0) && videoId == 0) return BadRequest("الرجاء ارفاق صورة الفيديو");
            var vid = new Video();
            if (videoId != 0)
            {
                vid = await _unitOfWork.Video.GetVideoByIdAsync(videoId);
                video.ImgUrl = vid.ImgUrl;
            }
            vid.ImgUrl = video.ImgUrl;
            vid.Descr = video.Descr;
            vid.LangCode = video.LangCode;
            vid.Link = video.Link;
            vid.Title = video.Title;

            vid.UploadedBy = User.GetUsername();
            var file = _files.Count > 0 ? _files[0] : null;
            await _unitOfWork.Video.HandleVideoAsyn(vid, file);
            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء الفيديو");
        }
        [HttpPost("get-pagged-videos")]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos([FromBody] PagingRequest pagingRequest)
        {
            var videos = await _unitOfWork.Video.GetVideosAsync(pagingRequest);
            return Ok(videos);
        }
        [HttpGet("get-video/{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _unitOfWork.Video.GetVideoByIdAsync(id);
            return Ok(video);
        }
        [HttpGet("latest-video")]
        public async Task<ActionResult<Video>> LatestVideo()
        {
            var video = await _unitOfWork.Video.GetLatestVideoAsync();
            return Ok(video);
        }
        [HttpPut("activate-video/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.Video.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل الفيديو");
        }

    }
}