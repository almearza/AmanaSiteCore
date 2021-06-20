using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models.VM;
using AmanaSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AmanaSite.Extensions;
using AmanaSite.Helpers.DataTables;
using AutoMapper;
using System;
namespace AmanaSite.Controllers.Api
{
    public class MobController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MobController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("handle-mob")]
        public async Task<ActionResult> HandleMob([FromForm] MAndFVM mobVM)
        {
            if (mobVM == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");

            var mobId = mobVM.Id;

            var _files = HttpContext.Request.Form.Files;
            if ((_files == null || _files.Count == 0) && mobId == 0) return BadRequest("الرجاء ارفاق صورة المبادرة - الفعالية");
            var mob = new MAndF();
            if (mobId != 0)
            {
                mob = await _unitOfWork.Mob.GetMobByIdAsync(mobId);
                mobVM.ImgUrl = mob.ImgUrl;
            }
            _mapper.Map(mobVM, mob);
            mob.Type=null;
            mob.UploadedBy = User.GetUsername();

            await _unitOfWork.Mob.HandleMobAsyn(mob, _files);
            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء المبادرة - الفعالية");
        }
        [HttpPost("get-pagged-mobs")]
        public async Task<ActionResult<IEnumerable<SlidersShowVM>>> GetMob([FromBody] PagingRequest pagingRequest)
        {
            var mob = await _unitOfWork.Mob.GetMobAsync(pagingRequest);
            return Ok(mob);
        }
        [HttpGet("get-mob/{id}")]
        public async Task<ActionResult<SlidersShowVM>> GetMob(int id)
        {
            var mob = await _unitOfWork.Mob.GetMobByIdAsync(id);
            return Ok(mob);
        }
        [HttpGet("get-types")]
        public async Task<ActionResult<SlidersShowVM>> GetMobTypes()
        {
            var _types = await _unitOfWork.Mob.GetMobTypes();
            return Ok(_types);
        }
        [HttpPut("activate-mob/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.Mob.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل المبادرة - الفعالية");
        }
    }
}