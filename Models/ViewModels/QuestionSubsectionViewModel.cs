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
    public class QuestionSubsectionViewModel: BaseViewModel
    {
        public Question Question {get;set;}
        public IEnumerable<Question> Questions {get;set;}
        public Subsection Subsection{get;set;} 
        public IEnumerable<Section> Sections{get;set;}      
        public IEnumerable<Subsection> Subsections{get;set;}
        public IEnumerable<QuestionSubsection> QuestionSubsections{get;set;}

        public int QuestionSubsectionID{get;set;}
        public int QuestionID{get;set;}
        public int SubsectionID{get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionsSelect {get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SubsectionsSelect {get;set;}
       
       
    }
}