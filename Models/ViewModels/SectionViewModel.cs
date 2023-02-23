using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;  
using ResearchData.Models;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class SectionViewModel: BaseViewModel
    {
        public Section Section {get;set;}
        public IEnumerable<Section> Sections {get;set;}
        public QuestionType QuestionType{get;set;}       
        public IEnumerable<QuestionType> QuestionTypes{get;set;}
        public Subsection Subsection {get;set;}
        public IEnumerable<Subsection> Subsections {get;set;}
       
    }
}