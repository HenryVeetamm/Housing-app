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
    public class HousingController : Controller
    {
        private readonly AppDbContext _context;

        public HousingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Housing
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Housings.Include(h => h.Owner);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Housing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Housings == null)
            {
                return NotFound();
            }

            var housing = await _context.Housings
                .Include(h => h.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (housing == null)
            {
                return NotFound();
            }

            return View(housing);
        }

        // GET: Admin/Housing/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/Housing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SquareMeters,RoomsCount,Description,Location,Amenities,IsAvailable,PictureUrl,Price,DealType,OwnerId")] Housing housing)
        {
            if (ModelState.IsValid)
            {
                housing.Id = Guid.NewGuid();
                _context.Add(housing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", housing.OwnerId);
            return View(housing);
        }

        // GET: Admin/Housing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Housings == null)
            {
                return NotFound();
            }

            var housing = await _context.Housings.FindAsync(id);
            if (housing == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", housing.OwnerId);
            return View(housing);
        }

        // POST: Admin/Housing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SquareMeters,RoomsCount,Description,Location,Amenities,IsAvailable,PictureUrl,Price,DealType,OwnerId")] Housing housing)
        {
            if (id != housing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(housing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousingExists(housing.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", housing.OwnerId);
            return View(housing);
        }

        // GET: Admin/Housing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Housings == null)
            {
                return NotFound();
            }

            var housing = await _context.Housings
                .Include(h => h.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (housing == null)
            {
                return NotFound();
            }

            return View(housing);
        }

        // POST: Admin/Housing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Housings == null)
            {
                return Problem("Entity set 'AppDbContext.Housings'  is null.");
            }
            var housing = await _context.Housings.FindAsync(id);
            if (housing != null)
            {
                _context.Housings.Remove(housing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousingExists(Guid id)
        {
          return (_context.Housings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
