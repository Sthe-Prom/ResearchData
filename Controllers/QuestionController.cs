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
using Microsoft.Extensions.Configuration;

namespace ResearchData.Controllers
{   
    public class QuestionController : Controller
    {
        //Private Fields
        //--------------

        private IQuestion context;
        private IAccount acc_ctx;
        public BaseViewModel BaseViewModel{get;set;}
        private IConfiguration Configuration;
        
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@";

        // public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString").ToString();
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString").ToString();
        
        
        //Constructor
        //-----------
        public QuestionController(IQuestion ctx, IAccount acc_ctx_, IConfiguration configuration)
        {
            context = ctx;
            acc_ctx = acc_ctx_;
            this.Configuration = configuration;
        }

        //READ
        public ViewResult Index()
        {
            QuestionViewModel vm = new QuestionViewModel();
            vm.Accounts = acc_ctx.Accounts;
            vm.Questions = context.Questions;
            return View(vm);
        }

        public ViewResult Manage()
        {
            QuestionViewModel vm = new QuestionViewModel();
            vm.Accounts = acc_ctx.Accounts;
            vm.Questions = context.Questions;
            return View(vm);
        }        

        //CREATE
        [HttpGet]
        public ViewResult Add() 
        {
            QuestionViewModel vm = new QuestionViewModel
            {                
                Surveys = getSurveys(),
                QuestionTypes = getQuestionTypes(),
                Accounts = acc_ctx.Accounts
            };
           
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(QuestionViewModel vm)
        {
            var Question = new Question
            {
                Question_ = vm.Question_,  
                QuestionAbbr =  vm.QuestionAbbr,              
                QuestionTypeID = vm.QuestionTypeID
            };            

            if(ModelState.IsValid)
            {
                context.SaveQuestion(Question);
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }
        
        //UPDATE
        public ViewResult Edit(int QuestionID)
        {
            QuestionViewModel vm = new QuestionViewModel
            {                
                Surveys = getSurveys(),
                QuestionTypes = getQuestionTypes(),
                Accounts = acc_ctx.Accounts
            };

            Question Question = context.Questions.FirstOrDefault(c => c.QuestionID == QuestionID);
           
            vm.Question_ = Question.Question_;  
            vm.QuestionAbbr = Question.QuestionAbbr;      
            vm.QuestionTypeID = Question.QuestionTypeID;
           
            return View(vm);
        }
                 

        [HttpPost]
        public IActionResult Edit(QuestionViewModel vm)
        {
          
            Question Question = context.Questions.FirstOrDefault(c => c.QuestionID == vm.QuestionID);

            if(Question != default(Question))
            {
                Question.Question_ =  vm.Question_ ;  
                Question.QuestionAbbr =  vm.QuestionAbbr; 
                Question.QuestionTypeID= vm.QuestionTypeID;
            }

            if(ModelState.IsValid)
            {
                context.SaveQuestion(Question);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        //DELETE
        public IActionResult Delete(int QuestionID)
        {
            Question Question = context.DeleteQuestion(QuestionID);

            if(Question != null)
            {               
                TempData["message"] = $"{Question.Question_} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
        public ViewResult QuestionList() => View(context.Questions);

         //D R O P - D O W N S
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getQuestionTypes()
        {
            List<QuestionType> models = new List<QuestionType>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select QuestionTypeID, QuestionTypeName from QuestionType";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new QuestionType();
                                m.QuestionTypeID = reader.GetInt32(reader.GetOrdinal("QuestionTypeID"));
                                m.QuestionTypeName = reader.GetString(reader.GetOrdinal("QuestionTypeName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList QTypeSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "QuestionTypeID", "QuestionTypeName");
            return QTypeSelect;
        }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getSurveys()
        {
            List<Survey> models = new List<Survey>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select SurveyID, SurveyName from Survey";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Survey();
                                m.SurveyID = reader.GetInt32(reader.GetOrdinal("SurveyID"));
                                m.SurveyName = reader.GetString(reader.GetOrdinal("SurveyName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList surveySelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SurveyID", "SurveyName");
            return surveySelect;
        }        
    }
}
