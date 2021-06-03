using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Models.VM
{
    public class NewsVM
    {
        public int Id { get; set; }
        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "فضلا أدخل العنوان")]
        public string Title { get; set; }
        [Display(Name = "وصف الخبر")]
        [Required(ErrorMessage = "فضلا أدخل وصف الخبر")]
        public string Descr { get; set; }
        [Display(Name = "مصدر الخبر")]
        [Required(ErrorMessage = "فضلا أدخل مصدر الخبر")]
        public string NewsResource { get; set; }
        [Display(Name = "الصورة 800*400")]
        public IFormFile NewsImg { get; set; }
        [Display(Name = "نوع الخبر")]
        [Required(ErrorMessage = "الرجاء إختيار نوع الخبر")]
        public int TypeId { get; set; }

        //for display
        public string ImgUrl { get; set; }
        public string UploadedBy { get; set; }

        public List<NewsType> Types { get; set; }
        [Display(Name = "اللغة")]
        [Required(ErrorMessage = "الرجاء إختيار اللغة")]
        public LangCode LangCode { get; set; }
    }
}