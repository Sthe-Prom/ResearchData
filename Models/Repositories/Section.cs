using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{   
    public class Section
    {
        [Key]
        public int SectionID { get; set; }

        [Required(ErrorMessage ="Please enter a Section Name")]
        public string SectionName { get; set; }
      
        /* Relationship
           FKs         
        */
     
        /* Ref. by */    
        public virtual ICollection<Subsection> Subsection { get; set; }

    }
}
