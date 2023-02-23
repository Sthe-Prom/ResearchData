using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

using System.Data.Odbc;
using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;


namespace ResearchData.Controllers
{
   
    public class SurveyController : BaseController<SurveyController>
    {
        //Private Fields
        //--------------

        private ISurvey context;
        private IAccount acc_context;
        public IConfiguration Configuration;
        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@";
        
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        //string strConn = configuration["Data:ResearchData:ConnectionString"];

        //public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString");
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString");
        

        //Constructor
        //-----------
        public SurveyController(ISurvey ctx, IAccount acc_context_, IConfiguration configuration)
        {
            context = ctx; 
            acc_context = acc_context_;
            this.Configuration = configuration;
        }

        //READ
        public ViewResult Index()
        {
             SurveyViewModel vm = new SurveyViewModel
            {                
                AccountsSelect = getAccounts(),
                Surveys = context.Surveys,
                Accounts = acc_context.Accounts     
            };

            return View(vm);
        }
      
            //CREATE
        [HttpGet]
        public ViewResult Add()
        {
            SurveyViewModel vm = new SurveyViewModel
            {                
                AccountsSelect = getAccounts(),
                Accounts = acc_context.Accounts                 
            };
           
            return View(vm);
        }    

        [HttpPost]
        public async Task<IActionResult> Add(SurveyViewModel vm)
        {
            var Survey = new Survey
            {
                SurveyName = vm.SurveyName,
                SurveyDate = System.DateTime.Now,
                AccountID = vm.AccountID
            };

            if(ModelState.IsValid)
            {
                context.SaveSurvey(Survey);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int SurveyID)
        {
            SurveyViewModel vm = new SurveyViewModel
            {                
                AccountsSelect = getAccounts(),        
                Accounts =  acc_context.Accounts    
            };

            Survey Survey = context.Surveys.FirstOrDefault(c => c.SurveyID == SurveyID);
         
            vm.SurveyName = Survey.SurveyName;
            vm.SurveyDate = Survey.SurveyDate;
            vm.AccountID = Survey.AccountID;
           
            return View(vm);
        }
    
        [HttpPost]
        public IActionResult Edit(SurveyViewModel vm)
        {         
            Survey Survey = context.Surveys.FirstOrDefault(c => c.SurveyID == vm.SurveyID);

            if(Survey != default(Survey))
            {
                Survey.SurveyName = vm.SurveyName;
                Survey.SurveyDate = vm.SurveyDate;
                Survey.AccountID = vm.AccountID;
                                
            }

            if(ModelState.IsValid)
            {
                context.SaveSurvey(Survey);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Survey);
            }
        }

        //DELETE
        public IActionResult Delete(int SurveyID)
        {
            Survey Survey = context.DeleteSurvey(SurveyID);

            if(Survey != null)
            {               
                TempData["message"] = $"{Survey.SurveyName} was deleted";
            }

            return RedirectToAction("Index");
        }       

        
        
         public ViewResult SurveyList() => View(context.Surveys);
        

        //D R O P - D O W N S
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getAccounts()
        {
            List<Account> models = new List<Account>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select AccountID, Name from Account";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Account();
                                m.AccountID = reader.GetInt32(reader.GetOrdinal("AccountID"));
                                m.Name = reader.GetString(reader.GetOrdinal("Name"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList AccSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "AccountID", "Name");
            return AccSelect;
        }

   
    }
}

