using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
    #nullable enable
    public class Address
    {       
        [Key]
        public int AddressID { get; set; }
        public string? StreetNr { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        
        /* Relationship
           FKs         
        */       
        [Required(ErrorMessage ="Please Enter Subunit")]
        public int SubunitID { get; set; }           

        /*Ref Nav properties*/
        [ForeignKey("SubunitID")]
        public virtual Subunit Subunit {get; set;}
       
    }
}
