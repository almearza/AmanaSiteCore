using System;
using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models
{
    public class Info
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string DoneBy { get; set; }
        public LangCode LangCode { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}