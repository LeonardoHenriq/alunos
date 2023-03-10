using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alunos_app.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ILogger<AlunoController> _logger;

        public AlunoController(ILogger<AlunoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
