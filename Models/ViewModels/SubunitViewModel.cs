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
    #nullable enable
    public class SubunitViewModel: BaseViewModel
    {
        public int SubunitID {get;set;}
        
        public string? SubunitName {get;set;}
      
        public string? SubunitDesc {get;set;}
      
        public int? SubunitDeptNr {get;set;}
      
        public IFormFile? SubunitLogo {get;set;}           
      
        public string? SubunitEmail {get;set;}   

        public string? SubunitCell {get;set;}        

        public string? SubunitServerAddress {get;set;}  

        
        public IEnumerable<Subunit>? Subunits {get; set;}
      
    }
}