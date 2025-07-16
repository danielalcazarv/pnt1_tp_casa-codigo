using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using casa_codigo_cursos.Context;
using casa_codigo_cursos.Models;

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
        public async Task<IActionResult> Create([Bind("InscripcionId,CursoId,AlumnoID,CodigoLetra")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,CursoId,AlumnoID,CodigoLetra")] Inscripcion inscripcion)
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

        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}
