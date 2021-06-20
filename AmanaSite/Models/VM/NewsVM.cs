using System;
using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models.VM
{
    public class NewsVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "العنوان حقل إجباري")]
        public string Title { get; set; }
        [Required(ErrorMessage = "الوصف حقل إجباري")]
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public string TypeName { get; set; }
        [Required(ErrorMessage = "النوع حقل إجباري")]
        public int TypeId { get; set; }
        public DateTime NewsDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "مصدر الخبر حقل إجباري")]
        public string NewsResource { get; set; }
        public string Lang { get; set; }
        [Required(ErrorMessage = "اللغة حقل إجباري")]
        public LangCode LangCode { get; set; }
    }
}