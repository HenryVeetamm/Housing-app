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
    public class ContractController : Controller
    {
        private readonly AppDbContext _context;

        public ContractController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contract
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Contracts.Include(c => c.HousingUnit).Include(c => c.Lessee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Contract/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.HousingUnit)
                .Include(c => c.Lessee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Admin/Contract/Create
        public IActionResult Create()
        {
            ViewData["HousingUnitId"] = new SelectList(_context.Housings, "Id", "Amenities");
            ViewData["LesseeId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/Contract/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HousingUnitId,LesseeId,MonthlyRent,StartMonth,StartYear,EndMonth,EndYear")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                contract.Id = Guid.NewGuid();
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HousingUnitId"] = new SelectList(_context.Housings, "Id", "Amenities", contract.HousingUnitId);
            ViewData["LesseeId"] = new SelectList(_context.Users, "Id", "FirstName", contract.LesseeId);
            return View(contract);
        }

        // GET: Admin/Contract/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["HousingUnitId"] = new SelectList(_context.Housings, "Id", "Amenities", contract.HousingUnitId);
            ViewData["LesseeId"] = new SelectList(_context.Users, "Id", "FirstName", contract.LesseeId);
            return View(contract);
        }

        // POST: Admin/Contract/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,HousingUnitId,LesseeId,MonthlyRent,StartMonth,StartYear,EndMonth,EndYear")] Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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
            ViewData["HousingUnitId"] = new SelectList(_context.Housings, "Id", "Amenities", contract.HousingUnitId);
            ViewData["LesseeId"] = new SelectList(_context.Users, "Id", "FirstName", contract.LesseeId);
            return View(contract);
        }

        // GET: Admin/Contract/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.HousingUnit)
                .Include(c => c.Lessee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Admin/Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'AppDbContext.Contracts'  is null.");
            }
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(Guid id)
        {
          return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
