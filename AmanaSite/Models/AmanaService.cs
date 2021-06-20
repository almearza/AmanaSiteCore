using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class AmanaService
    {
        public int Id { get; set; }
        public string Descr { get; set; }
       
        public string Link { get; set; }
        public string ImgUrl { get; set; }
        public int TypeId { get; set; }
        public ServiceType Type { get; set; }
        public DateTime UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        public LangCode LangCode { get; set; }
    }
   
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }   
}