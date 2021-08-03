// using System.Collections.Generic;
// using System.Threading.Tasks;
// using AmanaSite.Interfaces;
// using AmanaSite.Models.VM;
// using AmanaSite.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using AmanaSite.Extensions;
// using AmanaSite.Helpers.DataTables;
// using AutoMapper;
// using System;

// namespace AmanaSite.Controllers.Api
// {
//     [Authorize(Policy = "AdminLevel")]
//     public class AmanaServicesController : ApiBaseController
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IMapper _mapper;
//         public AmanaServicesController(IUnitOfWork unitOfWork, IMapper mapper)
//         {
//             this._mapper = mapper;
//             this._unitOfWork = unitOfWork;

//         }

//         [HttpPost("handle-service")]
//         public async Task<ActionResult> Handleservice([FromForm] AmanaServiceVM amanaServiceVM)
//         {
//             // IFormCollection model;
//             if (amanaServiceVM == null) return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");
//             if (!ModelState.IsValid)
//                 return BadRequest("الرجاء التأكد من إدخال كافة الحقول بالشكل الصحيح");


//             var serviceId = amanaServiceVM.Id;

//             var _files = HttpContext.Request.Form.Files;
            
//             if ((_files == null || _files.Count == 0) && serviceId == 0) return BadRequest("الرجاء ارفاق صورة الخدمة");
//             var service = new AmanaService();
//             if (serviceId != 0)
//             {
//                 service = await _unitOfWork.AmanaService.GetServiceByIdAsync(serviceId);
//                 amanaServiceVM.ImgUrl = service.ImgUrl;
//             }
//             _mapper.Map(amanaServiceVM, service);
//             service.Type=null;
//             service.UploadedBy = User.GetUsername();

//             await _unitOfWork.AmanaService.HandleServiceAsync(service, _files);
//             var result = await _unitOfWork.Complete();
//             if (result) return NoContent();
//             return BadRequest("حدث خطأ أثناء انشاء الخدمة");
//         }
//         [HttpPost("get-pagged-services")]
//         public async Task<ActionResult<IEnumerable<AmanaServiceVM>>> Getservice([FromBody] PagingRequest pagingRequest)
//         {
//             var service = await _unitOfWork.AmanaService.GetServicesAsync(pagingRequest);
//             return Ok(service);
//         }
//         [HttpGet("get-service/{id}")]
//         public async Task<ActionResult<AmanaServiceVM>> Getservice(int id)
//         {
//             var service = await _unitOfWork.AmanaService.GetServiceByIdAsync(id);
//             return Ok(service);
//         }
//         [HttpPut("activate-service/{id}")]
//         public async Task<ActionResult> Activate(int id)
//         {
//             await _unitOfWork.AmanaService.Activate(id);
//             if (await _unitOfWork.Complete())
//                 return NoContent();
//             return BadRequest("حدث خطأ أثناء تعطيل - تفعيل الخدمة");
//         }
//         [HttpGet("get-types")]
//         public async Task<ActionResult<IEnumerable<ServiceType>>> getServiceTypes()
//         {
//             var _types = await _unitOfWork.AmanaService.GetTypesAsync();
//             return Ok(_types);
//         }

//     }
// }