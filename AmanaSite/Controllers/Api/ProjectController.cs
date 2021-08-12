using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Helpers.DataTables;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AmanaSite.Controllers.Api
{
    [Authorize(Policy = "NewsLevel")]
    public class ProjectController:ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("handle-project")]
        public async Task<ActionResult> HandleProject([FromForm] Project model)
        {
            // IFormCollection model;
            if (model == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");


            var _files = HttpContext.Request.Form.Files;
            // var files = model.Files;
            if ((_files == null || _files.Count == 0) && model.Id == 0) return BadRequest("الرجاء ارفاق صورة الفيديو");
          
            var file = _files.Count > 0 ? _files[0] : null;
            await _unitOfWork.Project.HandleProjectAsyn(model, file);
            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء الفيديو");
        }
        [HttpPost("get-pagged-projects")]
        public async Task<ActionResult<IEnumerable<Project>>> Getprojects([FromBody] PagingRequest pagingRequest)
        {
            var projects = await _unitOfWork.Project.GetProjectsAsync(pagingRequest);
            return Ok(projects);
        }
        [HttpGet("get-project/{id}")]
        public async Task<ActionResult<Project>> Getproject(int id)
        {
            var project = await _unitOfWork.Project.GetProjectByIdAsync(id);
            return Ok(project);
        }
        [HttpGet("latest-6project")]
        public async Task<ActionResult<Project>> Latestproject()
        {
            var projects = await _unitOfWork.Project.GetLatest6ProjectsAsync();
            return Ok(projects);
        }
        [HttpPut("activate-project/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.Project.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل الفيديو");
        }
    }
}