using AmanaSite.Models.Survey;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyVM
    {
        //display
        public List<SurveyAge> SAges { get; set; }
        public List<SurveyEducation> SEducations { get; set; }
        public List<SurveyEvaluateEmployee> SEvalEmps { get; set; }
        public List<SurveyGender> SGenders { get; set; }
        public List<SurveyNationalty> SNats { get; set; }
        public List<SurveyReferencesType> SRefTypes { get; set; }
        public List<SurveyTransactionType> STransTypes { get; set; }
        public List<SurveyTransactionCompletion> STRanComps { get; set; }
        public List<SurveyVisitAvg> SurveyVisitAvgs { get; set; }

        //binding

        [Required(ErrorMessage ="Age")]
        public int SAgeId { get; set; }

        [Required(ErrorMessage = "Edu")]
        public int SEducationId { get; set; }
        [Required(ErrorMessage = "Rec")]
        public int SEvalEmpId { get; set; }
        [Required(ErrorMessage = "Gender")]
        public int SGenderId { get; set; }
        [Required(ErrorMessage = "Nat")]
        public int SNatId { get; set; }
        [Required(ErrorMessage = "RefType")]
        public int SRefTypeId { get; set; }
        [Required(ErrorMessage = "TranType")]
        public int STransTypeId { get; set; }
        [Required(ErrorMessage = "VisitCount")]
        public int SVisitAvgId { get; set; }
        [Required(ErrorMessage = "TranComp")]
        public int STRanCompId { get; set; }
        [MaxLength(500,ErrorMessage ="MaxLength")]
        public string Note { get; set; }
        public LangCode LangCode { get; set; }
    }
}