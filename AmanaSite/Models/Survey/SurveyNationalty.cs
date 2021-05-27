using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyNationalty
    {
        public int Id { get; set; }
        public string Nationalty { get; set; }
        public LangCode LangCode { get; internal set; }
    }
}