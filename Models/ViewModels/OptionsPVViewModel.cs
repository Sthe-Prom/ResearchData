using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class OptionsPVViewModel
    {
        public int SelectedOptionID { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> OptionsSelectN {get;set;}
    }

}