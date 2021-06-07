using System;

namespace AmanaSite.Models.VM
{
    public class NewsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public string TypeName { get; set; }
        public DateTime NewsDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        public string NewsResource { get; set; }
        public string Lang { get; set; }
    }
}