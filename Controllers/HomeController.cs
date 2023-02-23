using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResearchData.Models;
using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;

namespace ResearchData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAccount context;

        public HomeController(ILogger<HomeController> logger, IAccount ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            BaseViewModel vm = new BaseViewModel();
            vm.Accounts = context.Accounts;
            
            return View(vm);
        }

        public IActionResult ResearchData() => View();
        public IActionResult Contact()
        {
            BaseViewModel vm = new BaseViewModel();
            vm.Accounts = context.Accounts;
            
            return View(vm);
        }

        public IActionResult About()
        {
            BaseViewModel vm = new BaseViewModel();
            vm.Accounts = context.Accounts;
            
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public IActionResult Settings()
        {
            BaseViewModel vm = new BaseViewModel();
            vm.Accounts = context.Accounts;
            return View(vm);
        }
    }
}
