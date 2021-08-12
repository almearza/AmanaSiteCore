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
    [Authorize(Policy = "AdminLevel")]
    public class DocsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        [HttpPost("get-pagged-docs")]
        public async Task<ActionResult<IEnumerable<AmanaDocs>>> GetDocs([FromBody] PagingRequest pagingRequest)
        {
            var doc = await _unitOfWork.Docs.GetPaggedDocsAsync(pagingRequest);
            return Ok(doc);
        }
        [HttpPost("add-doc")]
        public async Task<ActionResult> Adddoc([FromForm] AmanaDocs model)
        {
            if (model == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");

            var _files = HttpContext.Request.Form.Files;
            if ((_files == null || _files.Count == 0)) return BadRequest("الرجاء ارفاق ملف الدليل");

            model.DoneBy = User.GetUsername();

            var addeddoc = await _unitOfWork.Docs.AddDocAsync(model, _files[0]);

            var result = await _unitOfWork.Complete();
            if (result) return Ok(addeddoc);

            return BadRequest("حدث خطأ أثناء حفظ الدليل");
        }

        [HttpDelete("delete-doc/{id}")]
        public async Task<ActionResult> Deletedoc(int id)
        {
            await _unitOfWork.Docs.DeleteDoc(id);

            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء حذف الدليل");
        }
    }
}