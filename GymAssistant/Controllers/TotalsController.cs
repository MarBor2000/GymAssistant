using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymAssistant.Data;
using GymAssistant.Models;

namespace GymAssistant.Controllers
{
    public class TotalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TotalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Total
        public async Task<IActionResult> Index(string sortBy)
        {
            ViewData["DataSort"] = string.IsNullOrEmpty(sortBy) ? "Data" : "";

            var data = from i in _context.Total select i;

            switch (sortBy)
            {
                default:
                    data = data.OrderByDescending(i => i.StartTraining);
                    break;
            }

            return _context.Total != null ?
                          View(await data.AsNoTracking().ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Total'  is null.");
        }




        // GET: Total/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Total/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("iD,StartTraining,lawa,przysiad,martwy,total")] Total totals)
        {
            totals.total = totals.lawa + totals.przysiad + totals.martwy;
            if (ModelState.IsValid)
            {
                _context.Add(totals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(totals);
        }

        // GET: Total/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Total == null)
            {
                return NotFound();
            }

            var totals = await _context.Total.FindAsync(id);
            if (totals == null)
            {
                return NotFound();
            }
            return View(totals);
        }

        // POST: Totals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("iD,StartTraining,lawa,przysiad,martwy,total")] Total totals)
        {
            totals.total = totals.lawa + totals.przysiad + totals.martwy;
            if (id != totals.iD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(totals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TotalsExists(totals.iD))
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
            return View(totals);
        }

        // GET: Totals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Total == null)
            {
                return NotFound();
            }

            var totals = await _context.Total
                .FirstOrDefaultAsync(m => m.iD == id);
            if (totals == null)
            {
                return NotFound();
            }

            return View(totals);
        }

        // POST: Totals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Total == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Totals'  is null.");
            }
            var totals = await _context.Total.FindAsync(id);
            if (totals != null)
            {
                _context.Total.Remove(totals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TotalsExists(int id)
        {
            return (_context.Total?.Any(e => e.iD == id)).GetValueOrDefault();
        }
    }
}
