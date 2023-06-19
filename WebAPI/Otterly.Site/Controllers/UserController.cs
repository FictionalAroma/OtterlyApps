using Microsoft.AspNetCore.Mvc;

namespace Otterly.Site.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
