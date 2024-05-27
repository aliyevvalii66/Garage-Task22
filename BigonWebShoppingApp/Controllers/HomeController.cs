using BigonWebShoppingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BigonWebShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
