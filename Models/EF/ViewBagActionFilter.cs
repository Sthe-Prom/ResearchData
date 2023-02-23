using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using ResearchData.Models;

using ResearchData.Models.Interfaces;
using System.ComponentModel;


namespace ResearchData.Models
{
    // public class ViewBagActionFilter : ActionFilterAttribute
    // {
    //     public IAccount context;

    //     public ViewBagActionFilter(IAccount ctx ){
    //         //DI will inject what you need here
    //         context = ctx;
    //     }

    //     public override void OnResultExecuting(ResultExecutingContext context)
    //     {
    //         // for Razor Views
    //         if (context.Controller is Controller)
    //         {
    //             var controller = context.Controller as Controller;
               
    //             controller.ViewBag.Accounts = ctx.Accounts;
               
    //         }

    //         base.OnResultExecuting(context);
    //     }
    // }
}