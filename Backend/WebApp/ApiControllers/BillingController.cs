using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BillingController(AppDbContext context, IMapper mapper, UserManager<App.Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Billing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Billing>>> GetBillings()
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            return await _context.Billings.Select(x => _mapper.Map<Billing>(x)).ToListAsync();
        }
        
        [HttpGet("unpaidbillings")]
        public async Task<ActionResult<IEnumerable<Billing>>> GetUnpaidBillings()
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            var query = _context.Billings.AsQueryable().AsNoTracking();
            var res = await query.Include(x => x.Contract)
                .ThenInclude(x => x!.Lessee).Include(x => x.Contract!.HousingUnit)
                .Where(x => x.Contract!.LesseeId == User.GetUserId() && !x.Payed).ToListAsync();
            return res.Select(x => _mapper.Map<Billing>(x)).ToList();
            
        }
        [HttpGet("paidbillings")]
        public async Task<ActionResult<IEnumerable<Billing>>> GetPaidBillings()
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            var query = _context.Billings.AsQueryable().AsNoTracking();
            var res = await query.Include(x => x.Contract)
                .ThenInclude(x => x!.Lessee).Include(x => x.Contract!.HousingUnit)
                .Where(x => x.Contract!.LesseeId == User.GetUserId() && x.Payed).ToListAsync();
            return res.Select(x => _mapper.Map<Billing>(x)).ToList();
            
        }

        // GET: api/Billing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Billing>> GetBilling(Guid id)
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);

            if (billing == null)
            {
                return NotFound();
            }

            return _mapper.Map<Billing>(billing);
        }

        // PUT: api/Billing/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilling(Guid id, Billing billing)
        {
            if (id != billing.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Billings.Update(_mapper.Map<App.Domain.Billing>(billing));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Billing
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Billing>> PostBilling(Billing billing)
        {
            if (_context.Billings == null)
            {
                return Problem("Entity set 'AppDbContext.Billings'  is null.");
            }

            var added = _context.Billings.Add(_mapper.Map<App.Domain.Billing>(billing));
            await _context.SaveChangesAsync();

            var mappedBack = _mapper.Map<Billing>(added.Entity);

            return CreatedAtAction("GetBilling", new { id = mappedBack.Id }, mappedBack);
        }
        
        
        [HttpGet("paybilling/{id}")]
        public async Task<ActionResult> PayBilling(Guid id)
        {
            if (_context.Billings == null)
            {
                return Problem("Entity set 'AppDbContext.Billings'  is null.");
            }

            var billing = await _context.Billings
                .Include(x => x.Contract).ThenInclude(x => x!.HousingUnit).ThenInclude(x => x!.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);
            billing!.Payed = !billing.Payed;

            var userWhoPays = await _userManager.FindByIdAsync(User.GetUserId().ToString());
            var userWhoGetsMoney = billing.Contract!.HousingUnit!.Owner;
            userWhoPays!.Money = userWhoPays.Money - billing.Contract.MonthlyRent;
            userWhoGetsMoney!.Money = userWhoGetsMoney.Money + billing.Contract.MonthlyRent;

            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Billing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilling(Guid id)
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }

            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingExists(Guid id)
        {
            return (_context.Billings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}