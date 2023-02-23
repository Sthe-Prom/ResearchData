using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{    
    public class Subsection
    {
        [Key]
        public int SubsectionID { get; set; }

        [Required(ErrorMessage ="Please Enter Sub-section Name")]
        public string SubsectionName { get; set; }

        /* Relationship
           FKs         
        */

        [Required(ErrorMessage ="Please Select Section")]
        public int SectionID { get; set; }
     
        [ForeignKey("SectionID")]
        public virtual Section Section { get; set; }

        /* Ref. by */
        public virtual ICollection<Question> Question { get; set; }

        public virtual ICollection<QuestionSubsection> QuestionSubsection { get; set; }


    }
}
