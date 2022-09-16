using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wedding.Models;

namespace Wedding.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly WeddingContext _context;

        public AttendancesController(WeddingContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var weddingContext = _context.Attendances.Include(a => a.Uu).Include(a => a.UuNavigation);
            return View(await weddingContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Uu)
                .Include(a => a.UuNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["Uuid"] = new SelectList(_context.Extras, "Uuid", "User");
            ViewData["Uuid"] = new SelectList(_context.Guests, "Uuid", "User");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                attendance.Uuid = Guid.NewGuid();
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Uuid"] = new SelectList(_context.Extras, "Uuid", "User", attendance.Uuid);
            ViewData["Uuid"] = new SelectList(_context.Guests, "Uuid", "User", attendance.Uuid);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["Uuid"] = new SelectList(_context.Extras, "Uuid", "User", attendance.Uuid);
            ViewData["Uuid"] = new SelectList(_context.Guests, "Uuid", "User", attendance.Uuid);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Uuid")] Attendance attendance)
        {
            if (id != attendance.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Uuid))
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
            ViewData["Uuid"] = new SelectList(_context.Extras, "Uuid", "User", attendance.Uuid);
            ViewData["Uuid"] = new SelectList(_context.Guests, "Uuid", "User", attendance.Uuid);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Uu)
                .Include(a => a.UuNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'WeddingContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(Guid id)
        {
          return (_context.Attendances?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }
    }
}
