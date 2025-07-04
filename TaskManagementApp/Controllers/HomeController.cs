using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
             

        public IActionResult Index()
        {
            ViewData["apiBaseUrl"] = configuration.GetSection("APIBaseURL").Value;
            return View();
        }

    }
}
