using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class BaladyaDescr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Descr { get; set; }
        public LangCode LangCode { get; set; }
    }
   
}