using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto_sistema_cft.Models;

namespace proyecto_sistema_cft.Controllers
{
    public class AsignaturasAsignadasController : Controller
    {
        private readonly DbSistemaCftContext _context;

        public AsignaturasAsignadasController(DbSistemaCftContext context)
        {
            _context = context;
        }

        // GET: AsignaturasAsignadas
        public async Task<IActionResult> Index()
        {
            var dbSistemaCftContext = _context.AsignaturasAsignadas.Include(a => a.Asignaturas).Include(a => a.Estudiantes);
            return View(await dbSistemaCftContext.ToListAsync());
        }

        // GET: AsignaturasAsignadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AsignaturasAsignadas == null)
            {
                return NotFound();
            }

            var asignaturasAsignada = await _context.AsignaturasAsignadas
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasAsignada == null)
            {
                return NotFound();
            }

            return View(asignaturasAsignada);
        }

        // GET: AsignaturasAsignadas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: AsignaturasAsignadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstudiantesId,AsignaturasId,Id,FechaRegistro")] AsignaturasAsignada asignaturasAsignada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignaturasAsignada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasAsignada.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasAsignada.EstudiantesId);
            return View(asignaturasAsignada);
        }

        // GET: AsignaturasAsignadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AsignaturasAsignadas == null)
            {
                return NotFound();
            }

            var asignaturasAsignada = await _context.AsignaturasAsignadas.FindAsync(id);
            if (asignaturasAsignada == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasAsignada.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasAsignada.EstudiantesId);
            return View(asignaturasAsignada);
        }

        // POST: AsignaturasAsignadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstudiantesId,AsignaturasId,Id,FechaRegistro")] AsignaturasAsignada asignaturasAsignada)
        {
            if (id != asignaturasAsignada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturasAsignada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasAsignadaExists(asignaturasAsignada.Id))
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
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasAsignada.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasAsignada.EstudiantesId);
            return View(asignaturasAsignada);
        }

        // GET: AsignaturasAsignadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AsignaturasAsignadas == null)
            {
                return NotFound();
            }

            var asignaturasAsignada = await _context.AsignaturasAsignadas
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasAsignada == null)
            {
                return NotFound();
            }

            return View(asignaturasAsignada);
        }

        // POST: AsignaturasAsignadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AsignaturasAsignadas == null)
            {
                return Problem("Entity set 'DbSistemaCftContext.AsignaturasAsignadas'  is null.");
            }
            var asignaturasAsignada = await _context.AsignaturasAsignadas.FindAsync(id);
            if (asignaturasAsignada != null)
            {
                _context.AsignaturasAsignadas.Remove(asignaturasAsignada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturasAsignadaExists(int id)
        {
          return (_context.AsignaturasAsignadas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
