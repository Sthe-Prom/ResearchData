using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;  
using ResearchData.Models;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class AnalyticsViewModel: BaseViewModel
    {
        //public IEnumerable<Account> Accounts {get;set;}
        public IEnumerable<Survey> Surveys {get;set;}  
        public IEnumerable<Question> Questions {get;set;}  
        public IEnumerable<QuestionOption> QuestionOptions {get;set;}    
    }
}