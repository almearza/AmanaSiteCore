using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Interfaces;
using AmanaSite.Models.Survey;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Repositories
{
    public class SurveyRepository : ISurvey
    {
        private readonly DContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentLang _currentLang;

        public SurveyRepository(DContext context, IMapper mapper, ICurrentLang currentLang)
        {
            this._currentLang = currentLang;
            this._mapper = mapper;
            this._context = context;
        }
        public void CreateSurvey(SurveyVM model)
        {
            var _ToBeSaveModel = _mapper.Map<SurveyData>(model);
            _ToBeSaveModel.UploadDate = DateTime.Now;
            _ToBeSaveModel.LangCode = _currentLang.Get();
            _context.SurveyData.Add(_ToBeSaveModel);
        }

        public async Task<SurveyVM> GetSetting()
        {
            var Ages = await _context.SurveyAge.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyEducations = await _context.SurveyEducation.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyEvaluateEmployee = await _context.SurveyEvaluateEmployee.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyGender = await _context.SurveyGender.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyNationalty = await _context.SurveyNationalty.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyReferencesType = await _context.SurveyReferencesType.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyTransactionCompletion = await _context.SurveyTransactionCompletion.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyTransactionType = await _context.SurveyTransactionType.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var SurveyVisitAvg = await _context.SurveyVisitAvg.Where(s => s.LangCode == _currentLang.Get()).ToListAsync();
            var _Survey = new SurveyVM
            {
                SAges = Ages,
                SEducations = SurveyEducations,
                SEvalEmps = SurveyEvaluateEmployee,
                SGenders = SurveyGender,
                SNats = SurveyNationalty,
                SRefTypes = SurveyReferencesType,
                STRanComps = SurveyTransactionCompletion,
                STransTypes = SurveyTransactionType,
                SurveyVisitAvgs = SurveyVisitAvg
            };
            return _Survey;
        }

        public async Task<IEnumerable<SurveyJson>> GetSurveys(DateTime fromDate, DateTime toDate)
        {
            var _survey = await _context.SurveyData
               .Where(s => s.UploadDate >= fromDate && s.UploadDate <= toDate)
               .Include(s => s.SAge)
               .Include(s => s.SEducation)
               .Include(s => s.SEvalEmp)
               .Include(s => s.SGender)
               .Include(s => s.SRefType)
               .Include(s => s.SNat)
               .Include(s => s.STransType)
               .Include(s => s.STRanComp)
               .Include(s => s.SVisitAvg)
               .Select(s => new SurveyJson
               {
                   Id = s.Id,
                   Gender = s.SGender.Gender,
                   AgeRange = s.SAge.AgeRange,
                   Education = s.SEducation.Education,
                   Evaluate = s.SEvalEmp.Evaluate,
                   Nationalty = s.SNat.Nationalty,
                   Note = s.Note,
                   SRefType = s.SRefType.ReferenceName,
                   TransactionCompletion = s.STRanComp.TransactionCompletion,
                   TransactionType = s.STransType.TransactionType,
                   UploadDate = s.UploadDate.ToString("dd/MM/yyyy"),
                   VisitAvg = s.SVisitAvg.VisitAvg
               }).ToListAsync();
            return _survey;
        }
    }
}