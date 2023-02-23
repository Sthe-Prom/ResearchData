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
    public class QuestionOptionController : Controller
    {
        //Private Fields
        //--------------

        private IQuestionOption context;
        private IQuestion q_context;
        private IAccount acc_ctx;
        private IConfiguration Configuration;
        
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@";

        // public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString");
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString");
        
        //Constructor
        //-----------
        public QuestionOptionController(IQuestionOption ctx, IQuestion q_ctx, IAccount acc_ctx_, IConfiguration configuration)
        {
            context = ctx;
            q_context = q_ctx;
            acc_ctx = acc_ctx_;
            this.Configuration = configuration;
        }

        //READ
        //public ViewResult Index() => View(context.QuestionOptions);
        public ViewResult Index()
        {           
            QuestionOptionViewModel vm = new QuestionOptionViewModel();
            vm.Questions = q_context.Questions;
            vm.QuestionOptions = context.QuestionOptions;           
            vm.QuestionSelect = getQuestions();
            //vm.QOptionVM2 = new QuestionOptionViewModel2();
            vm.Accounts = acc_ctx.Accounts;
            return View(vm);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add()
        {
            QuestionOptionViewModel vm = new QuestionOptionViewModel
            {                
                // Questions = q_context.Questions,
                // QuestionOptions = context.QuestionOptions,               
                QuestionSelect = getQuestions(),
                Accounts = acc_ctx.Accounts,              
                Questions = q_context.Questions       
            };
           
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(QuestionOptionViewModel vm)
        {
            var QuestionOption = new QuestionOption
            {
                Option = vm.Option,              
                QuestionID = vm.QuestionID
                   
            };

            if(ModelState.IsValid)
            {
                context.SaveQuestionOption(QuestionOption);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
        }
        
        //UPDATE
        public ViewResult Edit(int QuestionOptionID)
        {
            QuestionOptionViewModel2 vm = new QuestionOptionViewModel2
            {                
                QuestionSelect = getQuestions(),
                Accounts = acc_ctx.Accounts                
            };

            QuestionOption QOption = context.QuestionOptions.FirstOrDefault(c => c.QuestionOptionID == QuestionOptionID);
         
            vm.Option = QOption.Option;           
            vm.QuestionID = QOption.QuestionID;

            return View(vm);
        }      

        [HttpPost]
        public IActionResult Edit(QuestionOptionViewModel2 vm)
        {
            //  vm = new QuestionOptionViewModel2
            // {                
            //     QuestionSelect = getQuestions(),
            //     Accounts = acc_ctx.Accounts                
            // };

            QuestionOption QOption = context.QuestionOptions.
                        FirstOrDefault(c => c.QuestionOptionID == vm.QuestionOptionID);
            
           

             if(QOption != default(QuestionOption))
            {
                QOption.Option = vm.Option;               
                QOption.QuestionID = vm.QuestionID;                                
            }

            if(ModelState.IsValid)
            {
                context.SaveQuestionOption(QOption);
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }


        //DELETE
        public IActionResult Delete(int QuestionOptionID)
        {
            QuestionOption QuestionOption = context.DeleteQuestionOption(QuestionOptionID);

            if(QuestionOption != null)
            {               
                TempData["message"] = $"{QuestionOption.Option} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
        public ViewResult QuestionOptionList() => View(context.QuestionOptions);
        

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

            Microsoft.AspNetCore.Mvc.Rendering.SelectList QuesSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "QuestionID", "Question_");
            return QuesSelect;
        }

        
    }
}
