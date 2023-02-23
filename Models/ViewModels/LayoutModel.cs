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
    public class LayoutViewModel
    {
        public IEnumerable<Account> Accounts {get;set;}
        
    }
}