using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyTransactionType
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public LangCode LangCode { get; internal set; }
    }
}