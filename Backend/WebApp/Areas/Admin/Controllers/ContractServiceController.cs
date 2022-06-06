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
    public class ContractServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ContractServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ContractService
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ContractServices.Include(c => c.Contract).Include(c => c.Service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ContractService/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ContractServices == null)
            {
                return NotFound();
            }

            var contractService = await _context.ContractServices
                .Include(c => c.Contract)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractService == null)
            {
                return NotFound();
            }

            return View(contractService);
        }

        // GET: Admin/ContractService/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description");
            return View();
        }

        // POST: Admin/ContractService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContractId,ServiceId")] ContractService contractService)
        {
            if (ModelState.IsValid)
            {
                contractService.Id = Guid.NewGuid();
                _context.Add(contractService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", contractService.ContractId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", contractService.ServiceId);
            return View(contractService);
        }

        // GET: Admin/ContractService/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ContractServices == null)
            {
                return NotFound();
            }

            var contractService = await _context.ContractServices.FindAsync(id);
            if (contractService == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", contractService.ContractId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", contractService.ServiceId);
            return View(contractService);
        }

        // POST: Admin/ContractService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ContractId,ServiceId")] ContractService contractService)
        {
            if (id != contractService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractServiceExists(contractService.Id))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", contractService.ContractId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", contractService.ServiceId);
            return View(contractService);
        }

        // GET: Admin/ContractService/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ContractServices == null)
            {
                return NotFound();
            }

            var contractService = await _context.ContractServices
                .Include(c => c.Contract)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractService == null)
            {
                return NotFound();
            }

            return View(contractService);
        }

        // POST: Admin/ContractService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ContractServices == null)
            {
                return Problem("Entity set 'AppDbContext.ContractServices'  is null.");
            }
            var contractService = await _context.ContractServices.FindAsync(id);
            if (contractService != null)
            {
                _context.ContractServices.Remove(contractService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractServiceExists(Guid id)
        {
          return (_context.ContractServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
