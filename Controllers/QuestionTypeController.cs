using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;
using Microsoft.Extensions.Configuration;

namespace ResearchData.Controllers
{
   
    public class QuestionTypeController : Controller
    {
        //Private Fields
        //--------------

        private IQuestionType context;
        private IAccount acc_context;
        private IConfiguration Configuration;

        //Constructor
        //-----------
        public QuestionTypeController(IQuestionType ctx, IAccount acc_context_, IConfiguration configuration)
        {
            context = ctx;
            acc_context = acc_context_;
            this.Configuration = configuration;
        }

        //READ
        public ViewResult Index()
        {            
            QuestionTypeViewModel model = new QuestionTypeViewModel();
            model.QuestionTypes = context.QuestionTypes;
            model.Accounts = acc_context.Accounts;
            return View(model);

            //ProfileViewModel vm = new ProfileViewModel();
            // vm.Subunits = sec_context.Subunits;
            // return(vm);
        }
        // public ViewResult Index() => View();

        //CREATE
        [HttpGet]
        public ViewResult Add()
        {
            QuestionTypeViewModel vm = new QuestionTypeViewModel();
            vm.QuestionType = new QuestionType();
            vm.Accounts = acc_context.Accounts;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(QuestionTypeViewModel vm)
        {
            var QuestionType = new QuestionType
            {
                QuestionTypeName = vm.QuestionType.QuestionTypeName                
            };


            if(ModelState.IsValid)
            {
                context.SaveQuestionType(QuestionType);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int QuestionTypeID)
        {
            QuestionTypeViewModel vm = new QuestionTypeViewModel();
            vm.Accounts = acc_context.Accounts;

            QuestionType QuestionType = context.QuestionTypes.FirstOrDefault(c => c.QuestionTypeID == QuestionTypeID);
        
            vm.QuestionTypeName =  QuestionType.QuestionTypeName;
                      
            return View(vm);             
        }   

        [HttpPost]
        public IActionResult Edit(QuestionTypeViewModel vm)
        {
            QuestionType QuestionType = context.QuestionTypes.FirstOrDefault(c => c.QuestionTypeID == vm.QuestionTypeID);

             if(QuestionType != default(QuestionType))
            {
                QuestionType.QuestionTypeName = vm.QuestionTypeName;                                               
            }


            if(ModelState.IsValid)
            {
                context.SaveQuestionType(QuestionType);
                return RedirectToAction("Index");
            }
            else
            {
                return View(QuestionType);
            }
        }


        //DELETE
        public IActionResult Delete(int QuestionTypeID)
        {
            QuestionType QuestionType = context.DeleteQuestionType(QuestionTypeID);

            if(QuestionType != null)
            {               
                TempData["message"] = $"{QuestionType.QuestionTypeName} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
         public ViewResult QuestionTypeList() => View(context.QuestionTypes);
        

        
    }
}
