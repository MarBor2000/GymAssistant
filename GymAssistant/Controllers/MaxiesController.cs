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
    public class MaxiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaxiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Maxies
        public async Task<IActionResult> Index()
        {

            return _context.Maxy != null ?
                      View(await _context.Maxy.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.Maxy'  is null.");
        }

        [HttpGet]

        public async Task<IActionResult> Index(string search, string sortBy)
        {
            ViewData["NazwaCw"] = string.IsNullOrEmpty(sortBy) ? "name" : "";
            ViewData["Obciazenie"] = string.IsNullOrEmpty(sortBy) ? "wage" : "";
            ViewData["GetMaxy"] = search;
            var maxim = from x in _context.Maxy select x;


            switch (sortBy)
            {
                case "name":
                    maxim = maxim.OrderByDescending(x => x.name);
                    break;
                case "wage":
                    maxim = maxim.OrderBy(x => x.wage);
                    break;
                default:
                    maxim = maxim.OrderBy(x => x.name);
                    break;
            }


            if (!String.IsNullOrEmpty(search))
            {
                maxim = maxim.Where(x => x.name.Contains(search));
            }
            return View(await maxim.AsNoTracking().ToListAsync());
        }

        // GET: Maxies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maxy == null)
            {
                return NotFound();
            }

            var maxy = await _context.Maxy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maxy == null)
            {
                return NotFound();
            }

            return View(maxy);
        }

        // GET: Maxies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maxies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,wage,unit")] Maxy maxy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maxy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maxy);
        }

        // GET: Maxies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Maxy == null)
            {
                return NotFound();
            }

            var maxy = await _context.Maxy.FindAsync(id);
            if (maxy == null)
            {
                return NotFound();
            }
            return View(maxy);
        }

        // POST: Maxies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,wage,unit")] Maxy maxy)
        {
            if (id != maxy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maxy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaxyExists(maxy.Id))
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
            return View(maxy);
        }

        // GET: Maxies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Maxy == null)
            {
                return NotFound();
            }

            var maxy = await _context.Maxy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maxy == null)
            {
                return NotFound();
            }

            return View(maxy);
        }

        // POST: Maxies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Maxy == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Maxy'  is null.");
            }
            var maxy = await _context.Maxy.FindAsync(id);
            if (maxy != null)
            {
                _context.Maxy.Remove(maxy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaxyExists(int id)
        {
            return (_context.Maxy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}