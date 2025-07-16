using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using casa_codigo_cursos.Context;
using casa_codigo_cursos.Models;
using Microsoft.AspNetCore.Authorization;

namespace casa_codigo_cursos.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly CasaCodigoDbContext _context;

        public InscripcionesController(CasaCodigoDbContext context)
        {
            _context = context;
        }

        // GET: Inscripciones
        public async Task<IActionResult> Index()
        {
            var casaCodigoDbContext = _context.Inscripciones.Include(i => i.Curso);
            return View(await casaCodigoDbContext.ToListAsync());
        }

        // GET: Inscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // GET: Inscripciones/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscripcionId,CursoId,UsuarioID")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                inscripcion.FechaInscripcion = DateTime.Now;
                _context.Add(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcion.CursoId);
            return View(inscripcion);
        }

        // GET: Inscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcion.CursoId);
            return View(inscripcion);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,CursoId,UsuarioID")] Inscripcion inscripcion)
        {
            if (id != inscripcion.InscripcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionExists(inscripcion.InscripcionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcion.CursoId);
            return View(inscripcion);
        }

        // GET: Inscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion != null)
            {
                _context.Inscripciones.Remove(inscripcion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscribirse(int cursoId)
        {
            // Obtener el UsuarioId del usuario autenticado
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                TempData["Error"] = "No se pudo identificar al usuario autenticado.";
                return RedirectToAction("Index", "Cursos");
            }

            // Buscar usuario por UsuarioId
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
            if (usuario == null)
            {
                TempData["Error"] = "Usuario no encontrado.";
                return RedirectToAction("Index", "Cursos");
            }

            // Verificar que el curso existe
            var curso = await _context.Cursos.FindAsync(cursoId);
            if (curso == null)
            {
                TempData["Error"] = "Curso no encontrado.";
                return RedirectToAction("Index", "Cursos");
            }

            // Verificar que el usuario no esté ya inscripto en ese curso
            var yaInscripto = await _context.Inscripciones.AnyAsync(i => i.CursoId == cursoId && i.UsuarioID == userId);
            if (yaInscripto)
            {
                TempData["Error"] = "Ya estás inscripto en este curso.";
                return RedirectToAction("Index", "Cursos");
            }

            // Crear inscripción
            Inscripcion inscripcion = new Inscripcion();
            inscripcion.CursoId = cursoId;
            inscripcion.UsuarioID = userId;
            inscripcion.FechaInscripcion = DateTime.Now;


            try
            {
                _context.Inscripciones.Add(inscripcion);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Inscripción realizada con éxito.";
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = "Ocurrió un error al procesar tu inscripción. Inténtalo de nuevo.";
                return RedirectToAction("Index", "Cursos");
            }


        }

        // Muestra los cursos en los que el usuario está inscripto
        [Authorize]
        public async Task<IActionResult> MisCursos()
        {
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                TempData["Error"] = "No se pudo identificar al usuario autenticado.";
                return RedirectToAction("Index", "Cursos");
            }
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Curso)
                .Where(i => i.UsuarioID == userId)
                .ToListAsync();
            return View(inscripciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarBaja(int inscripcionId)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(inscripcionId);
            if (inscripcion == null)
            {
                TempData["Error"] = "No se encontró la inscripción.";
                return RedirectToAction("MisCursos");
            }
            
            if ((DateTime.Now - inscripcion.FechaInscripcion).TotalDays >= 30)
            {
                TempData["Error"] = "No puedes darte de baja después de 30 días.";
                return RedirectToAction("MisCursos");
            }
            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Te diste de baja del curso exitosamente.";
            return RedirectToAction("MisCursos");
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}
