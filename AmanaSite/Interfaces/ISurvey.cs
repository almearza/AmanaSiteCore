using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmanaSite.Models.Survey;

namespace AmanaSite.Interfaces
{
    public interface ISurvey
    {
        Task<IEnumerable<SurveyJson>> GetSurveys(DateTime fromDate, DateTime toDate);
        void CreateSurvey(SurveyVM model);
        Task<SurveyVM> GetSettingAsync();
    }
}