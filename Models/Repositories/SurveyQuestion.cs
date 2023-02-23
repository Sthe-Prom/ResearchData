using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ResearchData.Models.EF
{    
    public class SurveyQuestion
    {
        [Key]
        public int SurveyQuestionID { get; set; }              

        /* Relationship
           FKs         
        */

        [Required(ErrorMessage ="Please Select Survey")]
        public int SurveyID { get; set; }

        [Required(ErrorMessage ="Please Select Question")]
        public int QuestionID { get; set; }
     
        [ForeignKey("SurveyID")]
        public virtual Survey Survey { get; set; }

        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; } 
       
    }
}
