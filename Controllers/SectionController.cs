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
   
    public class SectionController : Controller
    {
        //Private Fields
        //--------------

        private ISection context;
        private ISubsection subs_ctx;
        private IQuestionType qtypes_ctx;  
        private IAccount acc_context;
        private IConfiguration Configuration;
        
        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@"; 
        //public string sOffCampuServer = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";

        // public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString");
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString");
        
              
        //Constructor
        //-----------
        public SectionController(ISection ctx_, ISubsection subs_ctx_, IQuestionType qtypes_ctx_, IAccount acc_context_, IConfiguration configuration)
        {
            context = ctx_;
            subs_ctx = subs_ctx_;
            qtypes_ctx = qtypes_ctx_;
            acc_context = acc_context_;
            this.Configuration = configuration;
        }

        //READ
        // public ViewResult Index() => View(context.Sections);
        public ViewResult Index() 
        {                      
            SubsectionViewModel model = new SubsectionViewModel();
            model.Sections = context.Sections;
            model.Accounts = acc_context.Accounts;

            //model.QuestionTypes = qtypes_ctx.QuestionTypes;          
            return View(model);

            //  SubsectionViewModel vm = new SubsectionViewModel
            // {
            //     SelectSections = getSections()
            // };

            // return View(vm);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add() 
        {
            SectionViewModel model = new SectionViewModel();         
            model.Accounts = acc_context.Accounts;

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Section Section)
        {
            if(ModelState.IsValid)
            {
                context.SaveSection(Section);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int SectionID)
        {
            SectionViewModel vm = new SectionViewModel();
           
            Section Section = context.Sections.FirstOrDefault(c => c.SectionID == SectionID);
        
            vm.Section.SectionName =  Section.SectionName;
            vm.Accounts = acc_context.Accounts;
             
            return View(vm);

        }
                  

        [HttpPost]
        public IActionResult Edit(SubsectionViewModel vm)
        {
            Section Section = context.Sections.FirstOrDefault(c => c.SectionID == vm.Section.SectionID);

             if(Section != default(Section))
            {
                Section.SectionName = vm.Section.SectionName;                                                
            }

            if(ModelState.IsValid)
            {
                context.SaveSection(Section);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Section);
            }
        }

        //DELETE
        public IActionResult Delete(int SectionID)
        {
            Section Section = context.DeleteSection(SectionID);

            if(Section != null)
            {               
                TempData["message"] = $"{Section.SectionName} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
        public ViewResult SectionList() => View(context.Sections);
        
          public Microsoft.AspNetCore.Mvc.Rendering.SelectList getSections()
        {
            List<Section> models = new List<Section>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select SectionName, SectionID from [Section]";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Section();
                                m.SectionID = reader.GetInt32(reader.GetOrdinal("SectionID"));
                                m.SectionName = reader.GetString(reader.GetOrdinal("SectionName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList SectionSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SectionID", "SectionName");
            return SectionSelect;
        }
    
        
    }
}
