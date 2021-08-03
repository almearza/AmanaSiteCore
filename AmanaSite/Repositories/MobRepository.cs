// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using AmanaSite.Data;
// using AmanaSite.Helpers.DataTables;
// using AmanaSite.Interfaces;
// using AmanaSite.Models;
// using AmanaSite.Models.VM;
// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;

// namespace AmanaSite.Repositories
// {
//     public class MobRepository : IMob
//     {
//         private readonly DContext _context;
//         private readonly IMapper _mapper;
//         private readonly IWebHostEnvironment _env;

//         public MobRepository(DContext context, IMapper mapper, IWebHostEnvironment env)
//         {
//             this._env = env;
//             this._mapper = mapper;
//             this._context = context;
//         }

//         public async Task HandleMobAsyn(MAndF model, IFormFileCollection files)
//         {

//             if (files.Count > 0 && files[0] != null)
//             {
//                 var file = files[0];
//                 var imgExt = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace("\"", "");
//                 var imgTitle = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + imgExt;
//                 var img_ServerSavePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "Mob");
//                 if (!Directory.Exists(img_ServerSavePath))
//                 {
//                     Directory.CreateDirectory(img_ServerSavePath);
//                 }
//                 var imgPathWithName = Path.Combine(img_ServerSavePath, imgTitle);
//                 using (Stream fileStream = new FileStream(imgPathWithName, FileMode.Create))
//                 {
//                     await file.CopyToAsync(fileStream);
//                 }
//                 model.ImgUrl = imgTitle;
//             }

//             model.MAndFDate = DateTime.Now;
//             model.Active = true;
//             if (model.Id == 0) await _context.MAndF.AddAsync(model);
//         }
//         public async Task Activate(int id)
//         {
//             var Mob = await _context.MAndF.FindAsync(id);
//             Mob.Active = !Mob.Active;
//         }

//         public async Task<PagingResponse<MAndFVM>> GetMobAsync(PagingRequest pagingRequest)
//         {
//             var query = _context.MAndF.OrderByDescending(n => n.MAndFDate).AsQueryable();

//             //searching
//             if (!string.IsNullOrEmpty(pagingRequest.search.value))
//             {
//                 query = query.Where(_Mob => _Mob.Title.Contains(pagingRequest.search.value));
//             }
//             //projecting and getting filtered list
//             return await PagingResponse<MAndFVM>
//             .GetPaggedList(pagingRequest, query.ProjectTo<MAndFVM>(_mapper.ConfigurationProvider).AsNoTracking());
//         }

//         public async Task<MAndF> GetMobByIdAsync(int id)
//         {
//             return await _context.MAndF.FindAsync(id);
//         }
//         public async Task<IEnumerable<MAndFType>> GetMobTypes()
//         {
//             return await _context.MAndFType.ToListAsync();
//         }
//     }
// }