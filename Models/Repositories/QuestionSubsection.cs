using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ResearchData.Models.EF
{    
    public class QuestionSubsection
    {
        [Key]
        public int QuestionSubsectionID { get; set; }              

        /* Relationship
           FKs         
        */

        [Required(ErrorMessage ="Please Select Question")]
        public int QuestionID { get; set; }

        [Required(ErrorMessage ="Please Select Subsection")]
        public int SubsectionID { get; set; }
     
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }      
     
        [ForeignKey("SubsectionID")]
        public virtual Subsection Subsection { get; set; }
       
    }
}
