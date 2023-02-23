using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class QuestionOptionViewModel: BaseViewModel
    {
        public int QuestionOptionID { get; set; }

        [Required(ErrorMessage ="Please Enter Question Option")]
        public string Option { get; set; }
       
        /* Relationship
           FKs         
        */

        [Required(ErrorMessage ="Please Select Question")]
        public int QuestionID { get; set; }

        public QuestionOption QuestionOption {get;set;}
        public Question Question {get;set;}
        
        public IEnumerable<Question> Questions {get; set;}
        public IEnumerable<QuestionOption> QuestionOptions {get; set;}

        public QuestionOptionViewModel2 QOptionVM2 {get;set;}
        
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionSelect {get;set;}
    }

}