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

        [Required(ErrorMessage ="الرجاء إختيار العمر")]
        public int SAgeId { get; set; }

        [Required(ErrorMessage = "الرجاء إختيار المؤهل الدراسي")]
        public int SEducationId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار تقيم موظف الاستقبال")]
        public int SEvalEmpId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار النوع")]
        public int SGenderId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار الجنسية")]
        public int SNatId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار الصفة التي تراجع بها الأمانة")]
        public int SRefTypeId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار تصنيف المعاملة التي تراجع من أجلها")]
        public int STransTypeId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار متوسط زيارتك للأمانة أسبوعيا")]
        public int SVisitAvgId { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار معدل التقدم في انجاز معاملتك عند كل مراجع")]
        public int STRanCompId { get; set; }
        [Display(Name ="ملاحظات")]
        public string Note { get; set; }
        public LangCode LangCode { get; set; }
    }
}