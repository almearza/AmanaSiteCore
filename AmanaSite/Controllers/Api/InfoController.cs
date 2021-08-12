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
    public class InfoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public InfoController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        [HttpPost("get-pagged-info")]
        public async Task<ActionResult<IEnumerable<Project>>> Getprojects([FromBody] PagingRequest pagingRequest)
        {
            var info = await _unitOfWork.Info.GetPaggedInfoAsync(pagingRequest);
            return Ok(info);
        }
        [HttpPost("add-info")]
        public async Task<ActionResult> AddInfo([FromForm] Info model)
        {
            if (model == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");

            var _files = HttpContext.Request.Form.Files;
            if ((_files == null || _files.Count == 0)) return BadRequest("الرجاء ارفاق صورة الانفوجرافك");

            model.DoneBy = User.GetUsername();

            var addedInfo =await _unitOfWork.Info.AddInfoAsync(model, _files[0]);

            var result = await _unitOfWork.Complete();
            if (result) return Ok(addedInfo);

            return BadRequest("حدث خطأ أثناء انشاء الانفوجرافك");
        }

        [HttpDelete("delete-info/{id}")]
        public async Task<ActionResult> DeleteInfo(int id)
        {
            await _unitOfWork.Info.DeleteInfo(id);

            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء حذف الانفوجرافك");
        }
    }
}