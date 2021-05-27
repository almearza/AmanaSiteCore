using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class MAndF
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public MAndFType Type { get; set; }
        public int TypeId { get; set; }
        public DateTime MAndFDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        public string Link { get; set; }
        public LangCode LangCode { get; set; }
    }
   
    public class MAndFType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}