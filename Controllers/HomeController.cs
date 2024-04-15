using ldl4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ldl4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
