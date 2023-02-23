using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
    #nullable enable
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        
        public string? Answer_ { get; set; }

        public int? SheetID { get; set; }

        /* Relationship
           FKs         
        */
        public int? OptionID { get; set; }

        public int? QuestionID { get; set; }
     
        [ForeignKey("OptionID")]
        public virtual QuestionOption QuestionOption { get; set; }

        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }      

    }
}
