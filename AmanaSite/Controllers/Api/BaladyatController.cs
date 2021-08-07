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
    [Authorize(Policy = "AdminLevel")]
    public class BaladyatController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BaladyatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("handle-Baladyat")]
        public async Task<ActionResult> HandleBaladyat([FromForm] Baladyat model)
        {
            if (model == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
            if (!ModelState.IsValid)
                return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");

            var BaladyatId = model.Id;


            var _baladya = new Baladyat();
            if (BaladyatId != 0)
            {
                _baladya = await _unitOfWork.Baladyat.GetBaladyaByIdAsync(BaladyatId);
            }
            _baladya = _mapper.Map<Baladyat>(model);
            

            _baladya.DoneBy = User.GetUsername();

            await _unitOfWork.Baladyat.HandleBaladyaAsync(_baladya);
            var result = await _unitOfWork.Complete();
            if (result) return NoContent();
            return BadRequest("حدث خطأ أثناء انشاء البلدية");
        }
        [HttpPost("get-pagged-Baladyats")]
        public async Task<ActionResult<IEnumerable<Baladyat>>> GetBaladyats([FromBody] PagingRequest pagingRequest)
        {
            var Baladyats = await _unitOfWork.Baladyat.GetBaladyatAsync(pagingRequest);
            return Ok(Baladyats);
        }
        [HttpGet("get-Baladyat/{id}")]
        public async Task<ActionResult<Baladyat>> GetBaladyat(int id)
        {
            var Baladyat = await _unitOfWork.Baladyat.GetBaladyaByIdAsync(id);
            return Ok(Baladyat);
        }
        [HttpPut("activate-Baladyat/{id}")]
        public async Task<ActionResult> Activate(int id)
        {
            await _unitOfWork.Baladyat.Activate(id);
            if (await _unitOfWork.Complete())
                return NoContent();
            return BadRequest("حدث خطأ أثناء تعطيل - تفعيل البلدية");
        }

    }

}