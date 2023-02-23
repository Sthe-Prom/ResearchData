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
   
    public class SurveyQuestionController : Controller
    {
        //Private Fields
        //--------------

        private ISurveyQuestion context;
        private IAccount acc_context;
        private ISurvey surv_context;
        private IQuestion q_context;
         private IConfiguration Configuration;

        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@"; 
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        
        // string sCampusServer = Configuration["Data:ResearchData:ConnectionString"];
        // string sOffCampuServer = Configuration["Data:ResearchData2:ConnectionString"];
        

        //Constructor
        //-----------
        public SurveyQuestionController(ISurveyQuestion ctx, IAccount acc_context_, ISurvey surv_context_, IQuestion q_context_, IConfiguration configuration)
        {
            context = ctx;
            acc_context = acc_context_;
            surv_context = surv_context_;
            q_context = q_context_;
            this.Configuration = configuration;
        }

        //READ
        // public ViewResult Index() => View(context.SurveyQuestions);
        public ViewResult Index()
        {
            SurveyQuestionViewModel vm = new SurveyQuestionViewModel
            {                
                SurveysSelect = getSurveys(),
                QuestionsSelect = getQuestions(),   
                SurveyQuestions = context.SurveyQuestions, 
                Surveys = surv_context.Surveys,
                Questions = q_context.Questions,
                Accounts = acc_context.Accounts
            };
           
            return View(vm);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add() 
        {
            SurveyQuestionViewModel vm = new SurveyQuestionViewModel
            {                
                SurveysSelect = getSurveys(),
                QuestionsSelect = getQuestions(),
                Accounts = acc_context.Accounts               
            };
           
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(SurveyQuestionViewModel vm)
        {
            var SurveyQuestion = new SurveyQuestion
            {
                SurveyID = vm.SurveyID,
                QuestionID = vm.QuestionID
            };

            if(ModelState.IsValid)
            {
                context.SaveSurveyQuestion(SurveyQuestion);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int SurveyQuestionID) 
        {
            SurveyQuestionViewModel vm = new SurveyQuestionViewModel
            {                
                SurveysSelect = getSurveys(),
                QuestionsSelect = getQuestions(),
                Accounts = acc_context.Accounts              
            };           
          
            SurveyQuestion SurveyQuestion = context.SurveyQuestions
                .FirstOrDefault(c => c.SurveyQuestionID == SurveyQuestionID);

                          
           
            vm.Survey.SurveyID = SurveyQuestion.SurveyID;
            vm.Question.QuestionID = SurveyQuestion.QuestionID;
          
            return View(vm);        
        }
                 

        [HttpPost]
        public IActionResult Edit(SurveyQuestionViewModel vm)
        {
            SurveyQuestion SurveyQuestion = context.SurveyQuestions
                .FirstOrDefault(c => c.SurveyQuestionID == vm.SurveyQuestionID);
           
            if(SurveyQuestion != default(SurveyQuestion))
            {
                 SurveyQuestion.SurveyID =  vm.SurveyID;
                 SurveyQuestion.QuestionID = vm.QuestionID;
            }
                            
            if(ModelState.IsValid)
            {
                context.SaveSurveyQuestion(SurveyQuestion);
                return RedirectToAction("Index");
            }
            else
            {
                return View(SurveyQuestion);
            }
        }

        //DELETE
        public IActionResult Delete(int SurveyQuestionID)
        {
            SurveyQuestion SurveyQuestion = context.DeleteSurveyQuestion(SurveyQuestionID);

            if(SurveyQuestion != null)
            {               
                TempData["message"] = $"{SurveyQuestion.SurveyQuestionID} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
         public ViewResult SurveyQuestionList() => View(context.SurveyQuestions);
        
        //D R O P - D O W N S

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

            Microsoft.AspNetCore.Mvc.Rendering.SelectList SurveySelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SurveyID", "SurveyName");
            return SurveySelect;
        }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getQuestions()
        {
            List<Question> models = new List<Question>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select QuestionID, Question_ from Question";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Question();
                                m.QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID"));
                                m.Question_ = reader.GetString(reader.GetOrdinal("Question_"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "QuestionID", "Question_");
            return QuestionSelect;
        }

        
    }
}
