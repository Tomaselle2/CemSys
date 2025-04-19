using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class FosasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
