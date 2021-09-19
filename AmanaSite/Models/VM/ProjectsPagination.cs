using System.Collections.Generic;

namespace AmanaSite.Models.VM
{
    public class ProjectsPagination
    {
         public IEnumerable<Project> Projects { get; set; }
        public int PageTotal { get; set; }
        public int ActivePageIndex { get; set; }
    }
}