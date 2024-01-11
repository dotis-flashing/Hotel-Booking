using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
