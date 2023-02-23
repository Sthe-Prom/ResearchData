using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class SubsectionViewModel: BaseViewModel
    {
        public int SubsectionID {get;set;}

        [Required(ErrorMessage ="Please enter Subsection Name:")]
        public string SubsectionName{get;set;}
         
        public IEnumerable<Section> Sections {get;set;}

        [Required(ErrorMessage ="Please enter Section Name:")]
        public int SectionID{get;set;}

        public Subsection Subsection {get;set;}
        
        public Section Section {get;set;}  
         
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectSections {get;set;}        
        
    }

}