using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmanaSite.Models.Survey
{
    public class SurveyTransactionCompletion
    {
        public int Id { get; set; }
        public string TransactionCompletion { get; set; }
        public LangCode LangCode { get; internal set; }
    }
}