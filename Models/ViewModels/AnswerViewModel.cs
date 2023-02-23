using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;  
using ResearchData.Models;
using ResearchData.Models.EF;
using Microsoft.AspNetCore.Mvc;


namespace ResearchData.Models
{
    public class AnswerViewModel: BaseViewModel
    {      
        public IEnumerable<Answer> Answers {get;set;}  
        public IEnumerable<Question> Questions {get;set;}  
        public IEnumerable<QuestionOption> QuestionOptions {get;set;}    


        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SurveysSelect {get;set;}

        // [Required("SelectedQID")]
        [Display(Name="Question")]
        public int SelectedQuestionID {get;set;}
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionsSelect {get;set;}
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> QuestionsSelect2 {get;set;}
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> QuestionsSelect4 {get;set;}
        public ActionResult QuestionsSelect3{get;set;}

        [Display(Name="Question Option")]
        public int SelectedOptionID {get;set;}
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> OptionsSelect2 { get; set; }
        public PartialViewResult OptionsSelect3 { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList OptionsSelect {get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList ScaleOptionsSelect {get;set;}

        //Model Variables
        public int AnswerID {get;set;}
        public string Answer {get;set;}
        public int SheetID {get;set;}
        public int SurveyID {get;set;}
        public int QuestionID {get;set;}
        public int OptionID {get;set;}
        public int ScaleOptionID {get;set;}

        public IFormFile ResponseFile {get;set;}  
    }
}