using casa_codigo_cursos.Context;
using casa_codigo_cursos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace casa_codigo_cursos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CasaCodigoDbContext _context;

        public HomeController(ILogger<HomeController> logger, CasaCodigoDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var cursosDestacados = _context.Cursos
                                .OrderBy(x => Guid.NewGuid())
                                .Take(6)
                                .ToList();

            return View(cursosDestacados);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
