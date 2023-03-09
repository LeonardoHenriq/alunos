using Microsoft.AspNetCore.Mvc;

namespace Alunos_app.Controllers
{
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
