using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;

using System.Data.Odbc;
using Microsoft.Data.SqlClient;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ResearchData.Controllers
{   
    public abstract partial class BaseController<T> : Controller where T: BaseController<T>
    {
        //Private Fields
        //--------------
        private ILogger<T> _logger;
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        
        public BaseViewModel BaseViewModel{get;set;}       

        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>()); 

    }
}
