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
   
    public class QuestionSubsectionController : Controller
    {
        //Private Fields
        //--------------

        private IQuestionSubsection context;
        private IQuestion q_context;
        private ISubsection subsec_context;
        private ISection sec_context;
        private IAccount acc_context;
        private IConfiguration Configuration;

        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@"; 
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";

        // public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString").ToString();
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString").ToString();
        
        
        //Constructor
        //-----------
        public QuestionSubsectionController(IQuestionSubsection ctx, IAccount acc_context_, IQuestion q_context_,
                                            ISubsection subsec_context_, ISection sec_context_, IConfiguration configuration)
        {
            context = ctx;
            acc_context = acc_context_;
            q_context = q_context_;
            subsec_context = subsec_context_;
            sec_context = sec_context_;
            this.Configuration = configuration;
        }

        //READ
        // public ViewResult Index() => View(context.QuestionSubsections);
        public ViewResult Index()
        {
            QuestionSubsectionViewModel vm = new QuestionSubsectionViewModel
            {                
                QuestionsSelect = getQuestions(),
                SubsectionsSelect = getSubsections(),   
                QuestionSubsections = context.QuestionSubsections,
                Questions = q_context.Questions,
                Subsections = subsec_context.Subsections,
                Sections = sec_context.Sections,
                Accounts = acc_context.Accounts
            };
           
            return View(vm);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add() 
        {
            QuestionSubsectionViewModel vm = new QuestionSubsectionViewModel
            {                
                QuestionsSelect = getQuestions(),
                SubsectionsSelect = getSubsections(),
                Accounts = acc_context.Accounts               
            };
           
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(QuestionSubsectionViewModel vm)
        {
            var QuestionSubsection = new QuestionSubsection
            {
                QuestionID = vm.QuestionID,
                SubsectionID = vm.SubsectionID
            };

            if(ModelState.IsValid)
            {
                context.SaveQuestionSubsection(QuestionSubsection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int QuestionSubsectionID) 
        {
            QuestionSubsectionViewModel vm = new QuestionSubsectionViewModel
            {                
                QuestionsSelect = getQuestions(),
                SubsectionsSelect = getSubsections(),
                Accounts = acc_context.Accounts              
            };           
          
            QuestionSubsection QuestionSubsections = context.QuestionSubsections
                .FirstOrDefault(c => c.QuestionSubsectionID == QuestionSubsectionID);

                          
           
            vm.QuestionID = QuestionSubsections.QuestionID;
            vm.SubsectionID = QuestionSubsections.SubsectionID;
          
            return View(vm);        
        }
                 

        [HttpPost]
        public IActionResult Edit(QuestionSubsectionViewModel vm)
        {
            QuestionSubsection QuestionSubsections = context.QuestionSubsections
                .FirstOrDefault(c => c.QuestionSubsectionID == vm.QuestionSubsectionID);
           
            if(QuestionSubsections != default(QuestionSubsection))
            {
                 QuestionSubsections.QuestionID =  vm.QuestionID;
                 QuestionSubsections.SubsectionID = vm.SubsectionID;
            }
                            

            if(ModelState.IsValid)
            {
                context.SaveQuestionSubsection(QuestionSubsections);
                return RedirectToAction("Index");
            }
            else
            {
                return View(QuestionSubsections);
            }
        }

        //DELETE
        public IActionResult Delete(int QuestionSubsectionID)
        {
            QuestionSubsection QuestionSubsection = context.DeleteQuestionSubsection(QuestionSubsectionID);

            if(QuestionSubsection != null)
            {               
                TempData["message"] = $"{QuestionSubsection.QuestionSubsectionID} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
         public ViewResult QuestionSubsectionList() => View(context.QuestionSubsections);
        
        //D R O P - D O W N S
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

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getSubsections()
        {
            List<Subsection> models = new List<Subsection>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select SubsectionID, SubsectionName from Subsection";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Subsection();
                                m.SubsectionID = reader.GetInt32(reader.GetOrdinal("SubsectionID"));
                                m.SubsectionName = reader.GetString(reader.GetOrdinal("SubsectionName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList SubsectionSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SubsectionID", "SubsectionName");
            return SubsectionSelect;
        }

        
    }
}
