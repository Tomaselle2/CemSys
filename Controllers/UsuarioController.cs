using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
