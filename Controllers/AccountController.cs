using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResearchData.Models;

using ResearchData.Models.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ResearchData.Models.EF;

using System.Web;
using System.Data.Odbc;
using Microsoft.Data.SqlClient;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace ResearchData.Controllers
{   
    public class AccountController: Controller
    {
        //private properties      
        private IAccount context;
        private readonly IWebHostEnvironment HostEnvironment;
        public BaseViewModel BaseViewModel{get;set;} 
        private IConfiguration Configuration; 
        
       
        //public string strConn = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        public string strConn = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@";

        //public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString").ToString();
        //public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString")ToString();
        
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;

        //Const
        public AccountController(IAccount ctx, IWebHostEnvironment he, 
                               SignInManager<User> s_man, UserManager<User> u_man, IConfiguration configuration)
        {         
            context = ctx;
            HostEnvironment = he;         
            signInManager = s_man;
            userManager = u_man;

            this.BaseViewModel = new BaseViewModel();
            this.BaseViewModel.Accounts = context.Accounts;
            this.ViewData["BaseViewModel"] = this.BaseViewModel;
            this.Configuration = configuration;
        }

        // public IActionResult Index()
        // {
        //     // ProfileViewModel vm = new ProfileViewModel();
        //     // vm.Subunits = subunit_ctx.Subunits;
        //     // return View(vm);    
        //     return View(context.Accounts);
        // }

        public ViewResult Index()
        {
            ProfileViewModel vm = new ProfileViewModel();
            vm.Accounts = context.Accounts;
            
            return View(vm);
        }

        public ViewResult myprofile()
        {
            // var userId = userManager.GetUserId(User);
            // ViewData["Msg"] = "Cant Get User ID";

            // if(signInManager.IsSignedIn(User))                                                 
            // {
            //     //var userId = user.Id;
            //     return View(context.Accounts.Where(c => c.Id == userId));
            // }   
            // else
            // {
            //     return View(context.Accounts);
            // } 

            // return View(context.Accounts);

            ProfileViewModel vm = new ProfileViewModel();
            vm.Accounts = context.Accounts;
            
            return View(vm);

        }
      
        public IActionResult Settings() => View();
        //public IActionResult Profile() => View();

        [HttpGet]
        public IActionResult Profile() 
        {
            // ProfileViewModel vm = new ProfileViewModel();
            // vm.Subunits = sec_context.Subunits;
            // return(vm);

            ProfileViewModel vm = new ProfileViewModel()
            {                
                Subunits = getSubunits(),   
                Users = getUsers(),
                Accounts = context.Accounts    
            };
           
             return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel vm)
        {
            string uploadedFileName = UploadedFile(vm);

            if(uploadedFileName == null){
                uploadedFileName = "Upload is null";
            }       

            string UserID_ = "";

            if(signInManager.IsSignedIn(User))                            
            {
                UserID_ = User.Identity.Name;
            }              

            var Account = new Account
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                ProfileImg = uploadedFileName,
                Cell = vm.Cell,   
                Id = vm.Id,
                SubunitID = vm.SubunitID
            };

            if(ModelState.IsValid)
            {
                await context.SaveAccount(Account);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Profile");
            }  
        }

        //UPDATE
        [HttpGet]
        public ViewResult Edit(int AccountID)
        {
            ProfileViewModel vm = new ProfileViewModel
            {
                Subunits = getSubunits(),
                Users = getUsers(),
                Accounts = context.Accounts
            };
            
            Account Account = context.Accounts.FirstOrDefault(c => c.AccountID == AccountID);  
            
            string uploadedFileName = UploadedFile(vm);  

            vm.Name = Account.Name;
            vm.Surname = Account.Surname;
            vm.Email = Account.Email;
            uploadedFileName = Account.ProfileImg;
            vm.Cell = Account.Cell; 
            vm.Id = Account.Id;
            vm.SubunitID = Account.SubunitID;

            return View(vm);        

        }  

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel vm)
        {
            string uploadedFileName = UploadedFile(vm);

            if(uploadedFileName == null){
                uploadedFileName = "Upload is null";
            }     

            var Account = context.Accounts.FirstOrDefault(c => c.AccountID == vm.AccountID);             
            if(Account != default(Account))
            {
                Account.Name = vm.Name;
                Account.Surname = vm.Surname;
                Account.Email = vm.Email;
                Account.Cell = vm.Cell;
                Account.ProfileImg = uploadedFileName;              
                Account.Id = vm.Id;
                Account.SubunitID = vm.SubunitID;                
            }  

            try
            {
                if(ModelState.IsValid)
                {
                    context.SaveAccount(Account);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vm);
                }
            }
            catch
            {
                return View(vm);
            }
        }

        //DELETE
        public IActionResult Delete(int AccountID)
        {
            Account Account = context.DeleteAccount(AccountID);

            if(Account != null)
            {               
                TempData["message"] = $"Account {Account.Name} was deleted";
            }

            return RedirectToAction("Index");
        }       

        private string UploadedFile(ProfileViewModel model)
        {
            string uniqueFileName = null;

            if(model.ProfileImg != null)
            {
                string uploadsFolder = Path.Combine( HostEnvironment.WebRootPath, "images/userprofile");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImg.CopyTo(fileStream);
                }
            }

            return uniqueFileName;

        }

        //D R O P - D O W N S
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getSubunits()
        {
            List<Subunit> models = new List<Subunit>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select SubunitID, SubunitName from Subunit";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Subunit();
                                m.SubunitID = reader.GetInt32(reader.GetOrdinal("SubunitID"));
                                m.SubunitName = reader.GetString(reader.GetOrdinal("SubunitName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList subunitSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SubunitID", "SubunitName");
            return subunitSelect;
        }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getUsers()
        {
            List<User> models = new List<User>();
                       
            using(SqlConnection connection = new SqlConnection(strConn)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select Id, Email from [User]";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new User();
                                m.Id = reader.GetString(reader.GetOrdinal("Id"));
                                m.Email = reader.GetString(reader.GetOrdinal("Email"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList userSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "Id", "Email");
            return userSelect;
        }

    }

    
}

