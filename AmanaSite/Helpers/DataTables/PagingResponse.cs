using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Helpers.DataTables
{
    public class PagingResponse<T>
    {
        public int Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public List<T> Data { get; set; }


        public PagingResponse(int draw, int recordsFiltered, int recordsTotal, List<T> data)
        {
            Draw = draw;
            RecordsFiltered = recordsFiltered;
            RecordsTotal = recordsTotal;
            Data = data;
        }
        public static async Task<PagingResponse<T>> GetPaggedList(PagingRequest pagingRequest, IQueryable<T> source)
        {
            var recordsTotal = await source.CountAsync();
            var recordsFiltered = recordsTotal;
            var data = await source.Skip(pagingRequest.start).Take(pagingRequest.length).ToListAsync();
            return new PagingResponse<T>(pagingRequest.draw,recordsTotal,recordsFiltered,data);
        }


    }
}