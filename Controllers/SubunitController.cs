using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResearchData.Models.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ResearchData.Models.EF;
using Microsoft.Extensions.Configuration;

namespace ResearchData.Controllers
{
   
    public class SubunitController : Controller
    {
        //Private Fields
        //--------------

        private ISubunit context;
        private IAccount acc_ctx;
        private readonly IWebHostEnvironment HostEnvironment;
        public BaseViewModel BaseViewModel{get;set;}  
        private IConfiguration Configuration;

        //Constructor
        //-----------
        public SubunitController(ISubunit ctx, IWebHostEnvironment hostEnvironment, IAccount acc_ctx_, IConfiguration configuration)
        {
            context = ctx;
            this.HostEnvironment = hostEnvironment;
            acc_ctx = acc_ctx_;
            this.Configuration = configuration;
        }

        //READ
        // public ViewResult Index() => View(context.Subunits);
        public ViewResult Index()
        {
            SubunitViewModel vm = new SubunitViewModel();
            vm.Subunits = context.Subunits;
            vm.Accounts = acc_ctx.Accounts;

            return View(vm);
        }

        //CREATE
        [HttpGet]
        public ViewResult Add() 
        {
            SubunitViewModel vm = new SubunitViewModel();
            vm.Subunits = context.Subunits;
            vm.Accounts = acc_ctx.Accounts;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubunitViewModel vm)
        {            
            string uploadedFileName = UploadedFile(vm);

            if(uploadedFileName == null){
                uploadedFileName = "Upload is null";
            }                  

            var Subunit = new Subunit
            {
                SubunitName = vm.SubunitName,
                SubunitDesc = vm.SubunitDesc,
                SubunitDeptNr = vm.SubunitDeptNr,
                SubunitLogo = uploadedFileName,
                SubunitEmail = vm.SubunitEmail,
                SubunitCell = vm.SubunitCell,
                SubunitServerAddress = vm.SubunitServerAddress

            };

            if(ModelState.IsValid)
            {
                await context.SaveSubunit(Subunit);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Add");
            }            

        }
        
        //UPDATE
        public ViewResult Edit(int SubunitID)
        {
            SubunitViewModel vm = new SubunitViewModel();            
                       
            View(context.Subunits.FirstOrDefault(c => c.SubunitID == SubunitID));

            Subunit Subunit = context.Subunits.FirstOrDefault(c => c.SubunitID == SubunitID);

            string uploadedFileName = UploadedFile(vm);
        
            vm.SubunitName = Subunit.SubunitName;
            vm.SubunitDesc = Subunit.SubunitDesc;
            vm.SubunitDeptNr = Subunit.SubunitDeptNr;
            uploadedFileName = Subunit.SubunitLogo;
            vm.SubunitEmail = Subunit.SubunitEmail;
            vm.SubunitCell = Subunit.SubunitCell;
            vm.SubunitServerAddress = Subunit.SubunitServerAddress;
            vm.Accounts = acc_ctx.Accounts;
           
            return View(vm);
        }      

        [HttpPost]
        public async Task<IActionResult> Edit(SubunitViewModel vm)
        {
            Subunit Subunit = context.Subunits.FirstOrDefault(c => c.SubunitID == vm.SubunitID);  

            string uploadedFileName = UploadedFile(vm);

            if(uploadedFileName == null){
                uploadedFileName = "Upload is null";
            }           
          
           if(Subunit != default(Subunit))
            {
                Subunit.SubunitName = vm.SubunitName;
                Subunit.SubunitDesc = vm.SubunitDesc;
                Subunit.SubunitDeptNr = vm.SubunitDeptNr;
                Subunit.SubunitLogo = uploadedFileName;
                Subunit.SubunitEmail = vm.SubunitEmail;
                Subunit.SubunitCell = vm.SubunitCell;
                Subunit.SubunitServerAddress = vm.SubunitServerAddress;
            }

             if(ModelState.IsValid)
                {
                    context.SaveSubunit(Subunit);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vm);
                }
           
        }

        //DELETE
        public IActionResult Delete(int SubunitID)
        {
            Subunit Subunit = context.DeleteSubunit(SubunitID);

            if(Subunit != null)
            {               
                TempData["message"] = $"{Subunit.SubunitName} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
        public ViewResult SubunitList() => View(context.Subunits);     


        private string UploadedFile(SubunitViewModel model)
        {
            string uniqueFileName = null;

            if(model.SubunitLogo != null)
            {
                string uploadsFolder = Path.Combine( HostEnvironment.WebRootPath, "images/subunit");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SubunitLogo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SubunitLogo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;

        }
        
    }
}
