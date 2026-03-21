using Microsoft.AspNetCore.Mvc;

namespace Semana1.Controllers
{
    public class NegociosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
