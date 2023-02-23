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
   
    public class AddressController : Controller
    {
        //Private Fields
        //--------------

        private IAddress context;
        
        //Constructor
        //-----------
        public AddressController(IAddress ctx)
        {
            context = ctx;
        }

        //READ
        // public ViewResult Index() => View(context.Addresss);
        public ViewResult Index() => View();

        //CREATE
        [HttpGet]
        public ViewResult Add() => View();

        [HttpPost]
        public IActionResult Add(Address Address)
        {
            if(ModelState.IsValid)
            {
                context.SaveAddress(Address);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //UPDATE
        public ViewResult Edit(int AddressID) =>
            View(context.Addresses
                .FirstOrDefault(c => c.AddressID == AddressID));      

        [HttpPost]
        public IActionResult Edit(Address Address)
        {
            if(ModelState.IsValid)
            {
                context.SaveAddress(Address);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Address);
            }
        }


        //DELETE
        public IActionResult Delete(int AddressID)
        {
            Address Address = context.DeleteAddress(AddressID);

            if(Address != null)
            {               
                TempData["message"] = $"{Address.StreetNr + Address.StreetName} was deleted";
            }

            return RedirectToAction("Index");
        }       
        
         public ViewResult AddressList() => View(context.Addresses);
        

        
    }
}
