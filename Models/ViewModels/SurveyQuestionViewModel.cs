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
    public class SurveyQuestionViewModel: BaseViewModel
    {
        public SurveyQuestion SurveyQuestion {get;set;}    
        public Question Question {get;set;}       
        public Survey Survey {get;set;}       
        public IEnumerable<Survey> Surveys {get;set;}
        public IEnumerable<Question> Questions{get;set;}
         public IEnumerable<SurveyQuestion> SurveyQuestions{get;set;}
           public int SurveyQuestionID{get;set;}
        public int SurveyID{get;set;}
        public int QuestionID{get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SurveysSelect {get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionsSelect {get;set;}
       
       
    }
}