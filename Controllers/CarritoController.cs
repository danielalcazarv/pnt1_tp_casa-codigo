using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using casa_codigo_cursos.Context;
using casa_codigo_cursos.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace casa_codigo_cursos.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly CasaCodigoDbContext _context;

        public CarritoController(CasaCodigoDbContext context)
        {
            _context = context;
        }

        // Obtiene el id del usuario autenticado
        private int GetUsuarioId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        // Agregar curso al carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int cursoId)
        {
            int usuarioId = GetUsuarioId();
            var carrito = await _context.Carritos.Include(c => c.CarritoCursos)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (carrito == null)
            {
                carrito = new Carrito { UsuarioId = usuarioId, CarritoCursos = new List<CarritoCurso>() };
                _context.Carritos.Add(carrito);
                await _context.SaveChangesAsync();
            }
            // Evitar duplicados
            if (!carrito.CarritoCursos.Any(cc => cc.CursoId == cursoId))
            {
                carrito.CarritoCursos.Add(new CarritoCurso { CursoId = cursoId });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Cursos");
        }

        // Ver carrito
        public async Task<IActionResult> Index()
        {
            int usuarioId = GetUsuarioId();
            var carrito = await _context.Carritos
                .Include(c => c.CarritoCursos)
                .ThenInclude(cc => cc.Curso)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            return View(carrito);
        }

        // Quitar curso del carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Quitar(int carritoCursoId)
        {
            var carritoCurso = await _context.CarritoCursos.FindAsync(carritoCursoId);
            if (carritoCurso != null)
            {
                _context.CarritoCursos.Remove(carritoCurso);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Checkout: inscribir a todos los cursos y vaciar el carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            int usuarioId = GetUsuarioId();
            var carrito = await _context.Carritos
                .Include(c => c.CarritoCursos)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (carrito == null || !carrito.CarritoCursos.Any())
            {
                TempData["Error"] = "El carrito está vacío.";
                return RedirectToAction("Index");
            }
            foreach (var cc in carrito.CarritoCursos)
            {
                // Evitar inscripciones duplicadas
                bool yaInscripto = await _context.Inscripciones.AnyAsync(i => i.CursoId == cc.CursoId && i.UsuarioID == usuarioId);
                if (!yaInscripto)
                {
                    _context.Inscripciones.Add(new Inscripcion { CursoId = cc.CursoId, UsuarioID = usuarioId });
                }
            }
            // Vaciar carrito
            _context.CarritoCursos.RemoveRange(carrito.CarritoCursos);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Inscripción realizada con éxito.";
            return RedirectToAction("Index", "Cursos");
        }
    }
} 