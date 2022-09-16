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
    public class GuestsController : Controller
    {
        private readonly WeddingContext _context;

        public GuestsController(WeddingContext context)
        {
            _context = context;
        }

        // GET: Guests
        public async Task<IActionResult> Index()
        {
            var weddingContext = _context.Guests.Include(g => g.CodeNavigation);
            return View(await weddingContext.ToListAsync());
        }

        // GET: Guests/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .Include(g => g.CodeNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            ViewData["Code"] = new SelectList(_context.Redeems, "Id", "Id");
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid,User,Code")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                guest.Uuid = Guid.NewGuid();
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Code"] = new SelectList(_context.Redeems, "Id", "Id", guest.Code);
            return View(guest);
        }

        // GET: Guests/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            ViewData["Code"] = new SelectList(_context.Redeems, "Id", "Id", guest.Code);
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Uuid,User,Code")] Guest guest)
        {
            if (id != guest.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.Uuid))
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
            ViewData["Code"] = new SelectList(_context.Redeems, "Id", "Id", guest.Code);
            return View(guest);
        }

        // GET: Guests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .Include(g => g.CodeNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Guests == null)
            {
                return Problem("Entity set 'WeddingContext.Guests'  is null.");
            }
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(Guid id)
        {
          return (_context.Guests?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }
    }
}
