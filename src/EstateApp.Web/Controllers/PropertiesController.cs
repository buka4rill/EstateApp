using System;
using EstateApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateApp.Web.Controllers
{
    public class PropertiesController : Controller
    {
        // Display list of properties page

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Add a property
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PropertyModel model)
        {
            throw new NotImplementedException();
        }
    }
}