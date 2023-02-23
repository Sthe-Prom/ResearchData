using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
    public class QuestionOption
    {
        [Key]
        public int QuestionOptionID { get; set; }

        [Required(ErrorMessage ="Please Enter Question Option")]
        public string Option { get; set; }
       
        /* Relationship
           FKs         
        */

        [Required(ErrorMessage ="Please Select Question")]
        public int QuestionID { get; set; }
       
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }

        /* Ref. by */
        public virtual ICollection<Answer> Answer { get; set; }

    }
}
