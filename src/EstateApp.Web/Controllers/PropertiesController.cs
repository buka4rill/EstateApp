using Microsoft.AspNetCore.Mvc;

namespace EstateApp.Web.Controllers
{
    public class PropertiesController : Controller
    {
        // Display list of properties page

        public IActionResult Index()
        {
            return View();
        }
    }
}