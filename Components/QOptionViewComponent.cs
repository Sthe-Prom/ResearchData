using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Components
{
    public class QOptionViewComponent: ViewComponent
    {        
        public IViewComponentResult Invoke()
        {           
            return View();
        }
    }
}


