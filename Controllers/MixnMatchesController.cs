using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WardRobe.Data;
using WardRobe.Models;

namespace WardRobe.Views.MixnMatches
{
    public class MixnMatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MixnMatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MixnMatches
        public async Task<IActionResult> Index()
        {
            return View(await _context.MixnMatch.ToListAsync());
        }

        // GET: MixnMatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mixnMatch = await _context.MixnMatch
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mixnMatch == null)
            {
                return NotFound();
            }

            return View(mixnMatch);
        }

        // GET: MixnMatches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MixnMatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Top,Bottom,UserId")] MixnMatch mixnMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mixnMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mixnMatch);
        }

        // GET: MixnMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mixnMatch = await _context.MixnMatch.FindAsync(id);
            if (mixnMatch == null)
            {
                return NotFound();
            }
            return View(mixnMatch);
        }

        // POST: MixnMatches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Top,Bottom,UserId")] MixnMatch mixnMatch)
        {
            if (id != mixnMatch.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mixnMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MixnMatchExists(mixnMatch.ID))
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
            return View(mixnMatch);
        }

        // GET: MixnMatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mixnMatch = await _context.MixnMatch
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mixnMatch == null)
            {
                return NotFound();
            }

            return View(mixnMatch);
        }

        // POST: MixnMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mixnMatch = await _context.MixnMatch.FindAsync(id);
            _context.MixnMatch.Remove(mixnMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MixnMatchExists(int id)
        {
            return _context.MixnMatch.Any(e => e.ID == id);
        }
    }
}
