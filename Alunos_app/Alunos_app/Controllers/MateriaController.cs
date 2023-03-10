using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alunos_app.Controllers
{
    public class MateriaController : Controller
    {
        private readonly ILogger<MateriaController> _logger;

        public MateriaController(ILogger<MateriaController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
