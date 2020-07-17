using System;
using System.Text;
using System.Threading.Tasks;
using EstateApp.Data.Entities;
using EstateApp.Web.Interfaces;
using EstateApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstateApp.Web.Controllers
{
    public class AccountsController : Controller
    {
        /**    
            Dependeny Injection - A way to get in objects/classes/services
            needed in a class into that particular class.
            We are able to do it because of the registration in line 67 of 
            Startup.cs
        **/
        // Contructor for class
        private readonly IAccountsService _accountsService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountsController(IAccountsService accountsService, SignInManager<ApplicationUser> signInManager)
        {
            _accountsService = accountsService;
            _signInManager = signInManager;
        }

        // Controller Action for Login Page
        // This one returns the View
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login Action
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View();
            // throw new NotImplementedException();

            try
            {
                // Log in User
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                // If sign in fails
                if (!result.Succeeded)
                {
                    // var errorMessages = AddErrors(result);
                    ModelState.AddModelError("", "Login failed, please check your details!");

                    return View();
                }

                // Else
                return LocalRedirect("~/");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

        }

        // Controller Action for Register Page
        // This one returns the View
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register Actions
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View();
            // throw new NotImplementedException();

            try
            {
                // Create User Account
                var user = await _accountsService.CreateUserAsync(model);

                // Sign User in
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Send user to homepage after creating account
                return LocalRedirect("~/");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // private string AddErrors(Microsoft.AspNetCore.Identity.SignInResult result)
        // {
        //     StringBuilder sb = new StringBuilder();
        //     foreach (var error in result.)
        //     {
        //         sb.Append(error.Description + " ");
        //     }

        //     return sb.ToString();
        // }
    }
}