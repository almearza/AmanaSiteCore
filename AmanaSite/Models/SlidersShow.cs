using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class SlidersShow
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string ToolImgUrl { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Active { get; set; }
        public string Link { get; set; }
        public bool CanExpire { get; set; }
        public DateTime? ExpireDate { get; set; }
        public LangCode LangCode { get; set; }
    }
}