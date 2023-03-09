using Microsoft.AspNetCore.Mvc;

namespace Alunos_app.Controllers
{
    public class MateriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
