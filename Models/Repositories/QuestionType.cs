using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
    public class QuestionType
    {
        [Key]
        public int QuestionTypeID { get; set; }

        [Required(ErrorMessage ="Please Enter a Question Type Name")]
        public string QuestionTypeName { get; set; }
     
        /* Relationship
           FKs         
        */
      
        /* Ref. by */       
        public virtual ICollection<Question> Question { get; set; }

    }
}
