using System;

namespace AmanaSite.Models.VM
{
    public class AmanaServiceVM
    {
        public int Id { get; set; }
        public string Descr { get; set; }

        public string Link { get; set; }
        public string ImgUrl { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public ServiceType Type { get; set; }
        public DateTime UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        public LangCode LangCode { get; set; }
        public string Lang { get; set; }
    }
}