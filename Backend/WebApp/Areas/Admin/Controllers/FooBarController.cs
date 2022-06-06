/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooBarController : Controller
    {
        private readonly AppDbContext _context;

        public FooBarController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/FooBar
        public async Task<IActionResult> Index()
        {
              return _context.FooBars != null ? 
                          View(await _context.FooBars.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.FooBars'  is null.");
        }

        // GET: Admin/FooBar/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FooBars == null)
            {
                return NotFound();
            }

            var fooBar = await _context.FooBars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fooBar == null)
            {
                return NotFound();
            }

            return View(fooBar);
        }

        // GET: Admin/FooBar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FooBar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FooBar fooBar)
        {
            if (ModelState.IsValid)
            {
                fooBar.Id = Guid.NewGuid();
                _context.Add(fooBar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fooBar);
        }

        // GET: Admin/FooBar/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FooBars == null)
            {
                return NotFound();
            }

            var fooBar = await _context.FooBars.FindAsync(id);
            if (fooBar == null)
            {
                return NotFound();
            }
            return View(fooBar);
        }

        // POST: Admin/FooBar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] FooBar fooBar)
        {
            if (id != fooBar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fooBar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FooBarExists(fooBar.Id))
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
            return View(fooBar);
        }

        // GET: Admin/FooBar/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FooBars == null)
            {
                return NotFound();
            }

            var fooBar = await _context.FooBars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fooBar == null)
            {
                return NotFound();
            }

            return View(fooBar);
        }

        // POST: Admin/FooBar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FooBars == null)
            {
                return Problem("Entity set 'AppDbContext.FooBars'  is null.");
            }
            var fooBar = await _context.FooBars.FindAsync(id);
            if (fooBar != null)
            {
                _context.FooBars.Remove(fooBar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FooBarExists(Guid id)
        {
          return (_context.FooBars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/
