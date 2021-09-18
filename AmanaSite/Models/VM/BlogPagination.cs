using System.Collections.Generic;

namespace AmanaSite.Models.VM
{
    public class BlogPagination
    {
        public IEnumerable<NewsVM> News { get; set; }
        public int PageTotal { get; set; }
        public int ActivePageIndex { get; set; }
    }
}