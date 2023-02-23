using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResearchData.Models;

using ResearchData.Models.Interfaces;
using System.ComponentModel;


namespace ResearchData.Controllers
{   
    // [Authorize (Roles="Admins")]
    public class UserAccountController : Controller
    {
        //private properties
        private UserManager<User> UserManager;
        private SignInManager<User> signInManager;
     
        //Const
        public UserAccountController(UserManager<User> userManager_, SignInManager<User> signInManager_)
        {
            UserManager = userManager_;
            signInManager = signInManager_;          
        }

        //Action Methods      
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel {ReturnUrl = returnUrl } );
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = await UserManager.FindByEmailAsync(loginModel.Email);

                if(user != null)
                {
                    //await signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if(result.Succeeded)
                    {
                        return Redirect(ReturnUrl ?? "/Home/About");
                    }
                    else
                        return Redirect(ReturnUrl ?? "/UserAccount/Login");
                }

                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid username or password");
            }

            ModelState.AddModelError(nameof(LoginModel.Email), "Invalid username or password");
            return View(loginModel);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

       

    }
}
