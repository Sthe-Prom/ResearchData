using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{    
    #nullable enable
    public class Survey
    {
        [Key]
        public int SurveyID { get; set; }
       
        public string SurveyName { get; set; }

        public DateTime SurveyDate { get; set; }

        /* Relationship
           FKs         
        */
    
         [Required(ErrorMessage ="Pleaese Select Account")]
        public int AccountID { get; set; }
        
        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }

        /* Ref. by */       
        #nullable disable
        public virtual ICollection<SurveyQuestion> SurveyQuestion { get; set; }
       
    }
}
