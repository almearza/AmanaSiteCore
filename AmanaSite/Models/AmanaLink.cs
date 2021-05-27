using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class AmanaLink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public LinkType Type { get; set; }
        public int TypeId { get; set; }
        public LangCode LangCode { get; set; }
    }
    public class LinkType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}