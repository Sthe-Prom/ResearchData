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
    public class SubsectionController : Controller
    {
        //Private Fields
        //--------------

        private ISubsection context;
        private IAccount acc_context;
        private IConfiguration Configuration;

        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@"; 
        //public string sOffCampuServer = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";

        // public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString");
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString");
        

        //Constructor
        //-----------
        public SubsectionController(ISubsection ctx, IAccount acc_context_, IConfiguration configuration)
        {
            context = ctx;           
            acc_context = acc_context_;
            this.Configuration = configuration;
        }

        //READ
        //public ViewResult Index() => View(context.Subsections);
        public ViewResult Index()
        {                          
            SubsectionViewModel2 vm = new SubsectionViewModel2();
            vm.Subsections = context.Subsections;
            vm.Accounts = acc_context.Accounts;
            vm.SelectSections = getSections();
            return View(vm); 
            
            // SubsectionViewModel vm = new SubsectionViewModel
            // {
            //     SelectSections = getSections()
            // };
            //Subsection s = new Subsection();
            

            //  return View(context.Subsections);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add()
        {
            SubsectionViewModel2 vm = new SubsectionViewModel2
            {
                SelectSections = getSections(),
                Accounts = acc_context.Accounts
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(SubsectionViewModel2 vm)
        {
             var Subsection = new Subsection
            {
                SubsectionName = vm.SubsectionName,                
                SectionID = vm.SectionID
            };
         
            if(ModelState.IsValid)
            {
                context.SaveSubsection(Subsection);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Add");
            }
        }
        
        //UPDATE
        public ViewResult Edit(int SubsectionID)
        {           
            SubsectionViewModel2 vm = new SubsectionViewModel2
            {
                SelectSections = getSections(),
                Accounts = acc_context.Accounts
            };

            Subsection Subsection = context.Subsections.FirstOrDefault(c => c.SubsectionID == SubsectionID);

            vm.SubsectionName = Subsection.SubsectionName;           
            vm.SectionID = Subsection.SectionID;
            
            return View(vm);     
        }
        
        [HttpPost]
        public IActionResult Edit(SubsectionViewModel2 vm)
        {

             Subsection Subsection = context.Subsections.FirstOrDefault(c => c.SubsectionID == vm.SubsectionID);

             if(Subsection != default(Subsection))
            {
                Subsection.SubsectionName = vm.SubsectionName;
                Subsection.SectionID = vm.SectionID;
                
            }

            if(ModelState.IsValid)
            {
                context.SaveSubsection(Subsection);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Subsection);
            }
        }


        //DELETE
        public IActionResult Delete(int SubsectionID)
        {
            Subsection Subsection = context.DeleteSubsection(SubsectionID);

            if(Subsection != null)
            {               
                TempData["message"] = $"{Subsection.SubsectionName} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
        public ViewResult SubsectionList() => View(context.Subsections);
        

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
