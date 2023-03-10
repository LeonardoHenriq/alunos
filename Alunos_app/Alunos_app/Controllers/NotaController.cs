using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alunos_app.Controllers
{
    public class NotaController : Controller
    {
        private readonly ILogger<NotaController> _logger;

        public NotaController(ILogger<NotaController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
