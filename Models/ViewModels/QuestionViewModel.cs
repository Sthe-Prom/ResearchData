using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ResearchData.Models.EF;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResearchData.Models
{
    public class QuestionViewModel: BaseViewModel
    {
        [Required]
        public int QuestionID { get; set; }

        [Required]
        public string Question_ { get; set; }

        [Required]
        //[Required(ErrorMessage = "Please Question Type")]
        public int QuestionTypeID { get; set; }

        // [Required]
        public int SurveyID { get; set; }

        public string QuestionAbbr { get; set; }

        // public QuestionType QuestionTypes {get;set;}

        // public Subsection Subsection {get;set;}

        // public Section Section {get;set;}

        public IEnumerable<Question> Questions {get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionTypes {get;set;}
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList Surveys {get;set;}

    }
}
