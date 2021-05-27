using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyReferencesType
    {
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public LangCode LangCode { get; internal set; }
    }
}