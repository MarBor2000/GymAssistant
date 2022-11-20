using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymAssistant.Data;
using GymAssistant.Models;

namespace NaOstro.Controllers
{
    public class JakZrobicMasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JakZrobicMasesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string sortBY)
        {
            ViewData["Sposob"] = string.IsNullOrEmpty(sortBY) ? "sposobik" : "";
            var masa = from x in _context.JakZrobicMase select x;


            switch (sortBY)
            {
                default:
                    masa = masa.OrderBy(x => x.Sposob);
                    break;
            }

            return _context.JakZrobicMase != null ?
                        View(await masa.AsNoTracking().ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.JakZrobicMase'  is null.");
        }

        // GET: JakZrobicMases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JakZrobicMase == null)
            {
                return NotFound();
            }

            var jakZrobicMase = await _context.JakZrobicMase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jakZrobicMase == null)
            {
                return NotFound();
            }

            return View(jakZrobicMase);
        }

        // GET: JakZrobicMases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JakZrobicMases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sposob,description")] JakZrobicMase jakZrobicMase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jakZrobicMase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jakZrobicMase);
        }

        // GET: JakZrobicMases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JakZrobicMase == null)
            {
                return NotFound();
            }

            var jakZrobicMase = await _context.JakZrobicMase.FindAsync(id);
            if (jakZrobicMase == null)
            {
                return NotFound();
            }
            return View(jakZrobicMase);
        }

        // POST: JakZrobicMases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sposob,description")] JakZrobicMase jakZrobicMase)
        {
            if (id != jakZrobicMase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jakZrobicMase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JakZrobicMaseExists(jakZrobicMase.Id))
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
            return View(jakZrobicMase);
        }

        // GET: JakZrobicMases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JakZrobicMase == null)
            {
                return NotFound();
            }

            var jakZrobicMase = await _context.JakZrobicMase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jakZrobicMase == null)
            {
                return NotFound();
            }

            return View(jakZrobicMase);
        }

        // POST: JakZrobicMases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JakZrobicMase == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JakZrobicMase'  is null.");
            }
            var jakZrobicMase = await _context.JakZrobicMase.FindAsync(id);
            if (jakZrobicMase != null)
            {
                _context.JakZrobicMase.Remove(jakZrobicMase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JakZrobicMaseExists(int id)
        {
            return (_context.JakZrobicMase?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}