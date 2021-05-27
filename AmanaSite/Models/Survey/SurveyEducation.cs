using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyEducation
    {
        public int Id { get; set; }
        public string Education { get; set; }
        public LangCode LangCode { get; set; }
    }
}