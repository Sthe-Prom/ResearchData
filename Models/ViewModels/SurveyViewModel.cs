using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResearchData.Models;
using ResearchData.Models.EF;

namespace ResearchData.Models
{    
    public class SurveyViewModel:BaseViewModel
    {
        public int SurveyID {get;set;}

        [Required(ErrorMessage ="Please enter Survey Name:")]
        public string SurveyName {get;set;}
              
        public DateTime SurveyDate {get;set;}   

        [Required(ErrorMessage ="Pleaese Select Account")]
        public int AccountID { get; set; }

        public IEnumerable<Survey> Surveys {get;set;}
       
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList AccountsSelect {get;set;}

    }
}