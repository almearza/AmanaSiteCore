using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AmanaSite.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Descr { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DoneBy { get; set; }
        public bool Active { get; set; }
        public LangCode LangCode { get; set; }

    }
}