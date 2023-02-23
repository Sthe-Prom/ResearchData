using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchData.Models.EF
{
#nullable enable
    public class Subunit
    {       
        [Key]
        public int SubunitID { get; set; }
        public string? SubunitName { get; set; }
        public string? SubunitDesc { get; set; }
        public int? SubunitDeptNr { get; set; }
        public string? SubunitLogo { get; set; }
        public string? SubunitEmail { get; set; }
        public string? SubunitCell { get; set; }
        public string? SubunitServerAddress { get; set; }

        /* Relationship
           FKs         
        */

        /* Ref. by */
        #nullable disable
        public virtual ICollection<Address> Address { get; set; }
       
        #nullable disable
        public virtual ICollection<Account> Account { get; set; }
       
    }
}
