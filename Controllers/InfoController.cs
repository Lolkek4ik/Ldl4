using Microsoft.AspNetCore.Mvc;

namespace ldl4.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
