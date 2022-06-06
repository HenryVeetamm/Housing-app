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
    public class BillingContractServiceController : Controller
    {
        private readonly AppDbContext _context;

        public BillingContractServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BillingContractService
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BillingContractServices.Include(b => b.Billing).Include(b => b.ContractService);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/BillingContractService/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.BillingContractServices == null)
            {
                return NotFound();
            }

            var billingContractService = await _context.BillingContractServices
                .Include(b => b.Billing)
                .Include(b => b.ContractService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingContractService == null)
            {
                return NotFound();
            }

            return View(billingContractService);
        }

        // GET: Admin/BillingContractService/Create
        public IActionResult Create()
        {
            ViewData["BillingId"] = new SelectList(_context.Billings, "Id", "Id");
            ViewData["ContractServiceId"] = new SelectList(_context.ContractServices, "Id", "Id");
            return View();
        }

        // POST: Admin/BillingContractService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BillingId,ContractServiceId,Amount,ServiceTotalSum")] BillingContractService billingContractService)
        {
            if (ModelState.IsValid)
            {
                billingContractService.Id = Guid.NewGuid();
                _context.Add(billingContractService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingId"] = new SelectList(_context.Billings, "Id", "Id", billingContractService.BillingId);
            ViewData["ContractServiceId"] = new SelectList(_context.ContractServices, "Id", "Id", billingContractService.ContractServiceId);
            return View(billingContractService);
        }

        // GET: Admin/BillingContractService/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.BillingContractServices == null)
            {
                return NotFound();
            }

            var billingContractService = await _context.BillingContractServices.FindAsync(id);
            if (billingContractService == null)
            {
                return NotFound();
            }
            ViewData["BillingId"] = new SelectList(_context.Billings, "Id", "Id", billingContractService.BillingId);
            ViewData["ContractServiceId"] = new SelectList(_context.ContractServices, "Id", "Id", billingContractService.ContractServiceId);
            return View(billingContractService);
        }

        // POST: Admin/BillingContractService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BillingId,ContractServiceId,Amount,ServiceTotalSum")] BillingContractService billingContractService)
        {
            if (id != billingContractService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingContractService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingContractServiceExists(billingContractService.Id))
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
            ViewData["BillingId"] = new SelectList(_context.Billings, "Id", "Id", billingContractService.BillingId);
            ViewData["ContractServiceId"] = new SelectList(_context.ContractServices, "Id", "Id", billingContractService.ContractServiceId);
            return View(billingContractService);
        }

        // GET: Admin/BillingContractService/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.BillingContractServices == null)
            {
                return NotFound();
            }

            var billingContractService = await _context.BillingContractServices
                .Include(b => b.Billing)
                .Include(b => b.ContractService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingContractService == null)
            {
                return NotFound();
            }

            return View(billingContractService);
        }

        // POST: Admin/BillingContractService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.BillingContractServices == null)
            {
                return Problem("Entity set 'AppDbContext.BillingContractServices'  is null.");
            }
            var billingContractService = await _context.BillingContractServices.FindAsync(id);
            if (billingContractService != null)
            {
                _context.BillingContractServices.Remove(billingContractService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingContractServiceExists(Guid id)
        {
          return (_context.BillingContractServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
