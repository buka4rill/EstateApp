using System;
using EstateApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateApp.Web.Controllers
{
    public class AccountsController : Controller
    {
        // Controller Action for Login Page
        // This one returns the View
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login Action
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View();
            throw new NotImplementedException();
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
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View();
            throw new NotImplementedException();
        }
    }
}