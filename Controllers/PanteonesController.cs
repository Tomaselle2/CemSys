using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class PanteonesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
