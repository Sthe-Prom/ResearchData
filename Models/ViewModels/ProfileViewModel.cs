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
    public class ProfileViewModel: BaseViewModel
    {
        public int AccountID {get;set;}

        [Required(ErrorMessage ="Please enter your Name:")]
        public string Name {get;set;}

        [Required(ErrorMessage = "Please enter your surname:")]
        public string Surname {get;set;}

        [Required (ErrorMessage = "Please enter your email:")]        
        public string Email {get;set;}   

        [Required(ErrorMessage = "Please upload Catalogue Image liugly:")]
        public IFormFile ProfileImg {get;set;} 
          
        [Required(ErrorMessage = "Please enter your Cell Number:")]
        public string Cell {get;set;}   

        [Required(ErrorMessage ="Please log in")]
        public string Id { get; set; }

        [Required(ErrorMessage ="Pleaese Select Subunit")]
        public int SubunitID { get; set; }

        //public IEnumerable<Subunit> Subunits {get; set;}

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList Subunits {get;set;} 

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList Users {get;set;}        
      

    }
}