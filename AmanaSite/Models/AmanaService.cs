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
    public class ServiceEval
    {
        public int Id { get; set; }
        public AmanaService AmanaService { get; set; }
        public int AmanaServiceId { get; set; }
        public DateTime VoteDate { get; set; }
        public EvalStar Eval { get; set; }
    }
    public enum EvalStar
    {
        poor=1,accepted=2,good=3,vgood=4,excellent=5
    }
}