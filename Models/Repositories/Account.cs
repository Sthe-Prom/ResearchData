using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
    
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter Your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Please Enter Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Your Cell")]
        public string Cell { get; set; }
       
         [Required(ErrorMessage ="Please Select unit logo")]
        public string ProfileImg { get; set; }

        /* Relationship
           FKs         
        */
      
        [Required(ErrorMessage ="Please enter user id")]
        public string Id { get; set; }
       
         [Required(ErrorMessage ="Please Enter Select SubunitID")]
        public int SubunitID { get; set; }
        
        // // /* Ref Nav Properties */
        [ForeignKey("SubunitID")]
        public virtual Subunit Subunit { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }

        /* Relationship */        
        public virtual ICollection<Survey> Survey { get; set; }

    }
}
