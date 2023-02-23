using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{   
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }

        [Required(ErrorMessage ="Please Enter Question")]       
        public string Question_ { get; set; }

        public string QuestionAbbr { get; set; }
     
        /* Relationship
           FKs         
        */
        [Required(ErrorMessage ="Please Select Question Type")]  
        public int QuestionTypeID { get; set; }
          
        [ForeignKey("QuestionTypeID")]
        public virtual QuestionType QuestionType { get; set; }

        /* Ref. by */      
        public virtual ICollection<QuestionOption> QuestionOption { get; set; }
        
        public virtual ICollection<QuestionSubsection> QuestionSubsection { get; set; }      

        public virtual ICollection<Answer> Answer { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestion { get; set; }

    }
}
