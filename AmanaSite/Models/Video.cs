using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmanaSite.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " حقل إجباري")]
        public string Title { get; set; }
        [Required(ErrorMessage = " حقل إجباري")]
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = " حقل إجباري")]
        public string Link { get; set; }
        [Required(ErrorMessage = " حقل إجباري")]
        public LangCode LangCode { get; set; }
        [NotMapped]
        public string Lang { get; set; }
    }
}