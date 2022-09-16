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
    public class RedeemsController : Controller
    {
        private readonly WeddingContext _context;

        public RedeemsController(WeddingContext context)
        {
            _context = context;
        }

        // GET: Redeems
        public async Task<IActionResult> Index()
        {
              return _context.Redeems != null ? 
                          View(await _context.Redeems.ToListAsync()) :
                          Problem("Entity set 'WeddingContext.Redeems'  is null.");
        }

        // GET: Redeems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Redeems == null)
            {
                return NotFound();
            }

            var redeem = await _context.Redeems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (redeem == null)
            {
                return NotFound();
            }

            return View(redeem);
        }

        // GET: Redeems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Redeems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code")] Redeem redeem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(redeem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(redeem);
        }

        // GET: Redeems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Redeems == null)
            {
                return NotFound();
            }

            var redeem = await _context.Redeems.FindAsync(id);
            if (redeem == null)
            {
                return NotFound();
            }
            return View(redeem);
        }

        // POST: Redeems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code")] Redeem redeem)
        {
            if (id != redeem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redeem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedeemExists(redeem.Id))
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
            return View(redeem);
        }

        // GET: Redeems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Redeems == null)
            {
                return NotFound();
            }

            var redeem = await _context.Redeems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (redeem == null)
            {
                return NotFound();
            }

            return View(redeem);
        }

        // POST: Redeems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Redeems == null)
            {
                return Problem("Entity set 'WeddingContext.Redeems'  is null.");
            }
            var redeem = await _context.Redeems.FindAsync(id);
            if (redeem != null)
            {
                _context.Redeems.Remove(redeem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedeemExists(int id)
        {
          return (_context.Redeems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
