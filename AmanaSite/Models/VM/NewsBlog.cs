using System.Collections.Generic;

namespace AmanaSite.Models.VM
{
    public class NewsBlog
    {
        public NewsVM NewsVM { get; set; }
        public IEnumerable<NewsVM> NexNews { get; set; }
    }
}