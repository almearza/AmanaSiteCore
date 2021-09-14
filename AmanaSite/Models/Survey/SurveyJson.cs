using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyJson
    {
        public int Id { get; set; }
        public string AgeRange { get; set; }
        public string Education { get; set; }
        public string Evaluate { get; set; }
        public string Gender { get; set; }
        public string Nationalty { get; set; }
        public string SRefType { get; set; }
        public string TransactionType { get; set; }
        public string TransactionCompletion { get; set; }
        public string VisitAvg { get; set; }
        public string Note { get; set; }
        public string UploadDate { get; set; }
    }
}