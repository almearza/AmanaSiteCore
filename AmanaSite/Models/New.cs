using System;
namespace AmanaSite.Models
{
    public class New
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public NewsType Type { get; set; }
        public int TypeId { get; set; }
        public DateTime NewsDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        public string NewsResource { get; set; }
        public LangCode LangCode { get; set; }
    }
   
}