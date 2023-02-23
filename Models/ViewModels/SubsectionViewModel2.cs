using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResearchData.Models;
using ResearchData.Models.EF;

namespace ResearchData.Models
{    
    public class SubsectionViewModel2: BaseViewModel
    {
        public int SubsectionID {get;set;}

        [Required(ErrorMessage ="Please enter Survey Name:")]
        public string SubsectionName {get;set;}
     
        [Required(ErrorMessage ="Pleaese Select SUbsection")]
        public int SectionID { get; set; }

        public IEnumerable<Subsection> Subsections {get;set;}

        public Section Section {get;set;}  
           public Subsection Subsection {get;set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectSections {get;set;}

    }
}