using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyData
    {
        public int Id { get; set; }
        public SurveyAge SAge { get; set; }
        public int SAgeId { get; set; }
        public SurveyEducation SEducation { get; set; }
        public int SEducationId { get; set; }
        public SurveyEvaluateEmployee SEvalEmp { get; set; }
        public int SEvalEmpId { get; set; }
        public SurveyGender SGender { get; set; }
        public int SGenderId { get; set; }
        public SurveyNationalty SNat { get; set; }
        public int SNatId { get; set; }
        public SurveyReferencesType SRefType { get; set; }
        public int SRefTypeId { get; set; }
        public SurveyTransactionType STransType { get; set; }
        public int STransTypeId { get; set; }
        public SurveyTransactionCompletion STRanComp { get; set; }
        public int STRanCompId { get; set; }
        public SurveyVisitAvg SVisitAvg { get; set; }
        public int SVisitAvgId { get; set; }
        public string NationalityOther { get; set; }
        public string Note { get; set; }
        public DateTime UploadDate { get; set; }
        public LangCode LangCode { get; set; }
    }
}