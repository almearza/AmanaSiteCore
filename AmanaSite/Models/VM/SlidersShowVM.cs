using System;
using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models.VM
{
    public class SlidersShowVM
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Descr { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Active { get; set; }
        public string Link { get; set; }
        public bool CanExpire { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Lang { get; set; }
         [Required(ErrorMessage = "اللغة حقل إجباري")]
        public LangCode LangCode { get; set; }
    }
}