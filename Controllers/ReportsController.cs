using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;

namespace ResearchData.Controllers
{
   
    public class ReportsController : Controller
    {
        //Private Fields
        //--------------
        private IQuestion q_ctx;
        private IQuestionOption qo_ctx;
        private ISurvey surv_ctx;
        private IAccount Acc_cts;
        
        //Constructor
        //-----------
        public ReportsController(IQuestion q_ctx_, IQuestionOption qo_ctx_, ISurvey surv_ctx_, IAccount Acc_cts_ )
        { 
            q_ctx = q_ctx_;
            qo_ctx = qo_ctx_;
            surv_ctx = surv_ctx_;
            Acc_cts = Acc_cts_;
        }

        //READ
        public ViewResult Index()
        {
            // ReportsViewModel vm = new ReportsViewModel();
            // vm.Questions = q_context.Questions;
            // vm.QuestionOptions = context.QuestionOptions;           
            // vm.QuestionSelect = getQuestions();
            // vm.QOptionVM2 = new ReportsViewModel();
            // return View(vm);

            ReportsViewModel vm = new ReportsViewModel();
            vm.Accounts = Acc_cts.Accounts;
            
            return View(vm);
        }

        
    }
}
