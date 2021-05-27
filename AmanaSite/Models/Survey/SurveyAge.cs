using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyAge
    {
        public int Id { get; set; }
        public string AgeRange { get; set; }
        public LangCode LangCode { get; set; }
    }
}