using System;
using System.Threading.Tasks;
using EstateApp.Web.Interfaces;
using EstateApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateApp.Web.Controllers
{
    public class PropertiesController : Controller
    {
        // Controller, Dependency Injection
        private readonly IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // Display list of properties page

        [HttpGet]
        public IActionResult Index()
        {
            // Properties
            var properties = _propertyService.GetAllProperties();
            return View(properties);
        }

        // Add a property
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PropertyModel model)
        {
            // throw new NotImplementedException();
            //  Validate model
            if (!ModelState.IsValid) return View();

            try
            {
                await _propertyService.AddProperty(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // Return an error message to accounts page
                ModelState.AddModelError("", e.Message);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}