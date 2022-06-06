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
    public class BillingContractServiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BillingContractServiceController(AppDbContext context, IMapper mapper, UserManager<App.Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/BillingContractService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.BillingContractService>>>
            GetBillingContractServices()
        {
            if (_context.BillingContractServices == null)
            {
                return NotFound();
            }

            return await _context.BillingContractServices
                .Select(x => _mapper.Map<PublicApi.DTO.v1.BillingContractService>(x!))
                .ToListAsync();
        }

        // GET: api/BillingContractService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.BillingContractService>> GetBillingContractService(Guid id)
        {
            if (_context.BillingContractServices == null)
            {
                return NotFound();
            }

            var billingContractService = await _context.BillingContractServices.FindAsync(id);

            if (billingContractService == null)
            {
                return NotFound();
            }

            return _mapper.Map<PublicApi.DTO.v1.BillingContractService>(billingContractService);
        }
        [HttpGet("detailedbilling/{id}")]
        public async Task<ActionResult<IEnumerable<BillingContractService>>> GetDetailedBilling(Guid id)
        {
            if (_context.Billings == null)
            {
                return NotFound();
            }

            var res = await _context.BillingContractServices.Include(x => x.ContractService)
                .ThenInclude(x => x!.Service)
                .Where(x => x.BillingId == id).ToListAsync();

            return res.Select(x => _mapper.Map<BillingContractService>(x)).ToList();
        }

        // PUT: api/BillingContractService/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillingContractService(Guid id,
            PublicApi.DTO.v1.BillingContractService billingContractService)
        {
            if (id != billingContractService.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
                _context.BillingContractServices
                    .Update(_mapper.Map<App.Domain.BillingContractService>(billingContractService));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingContractServiceExists(id))
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

        // POST: api/BillingContractService
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillingContractService>> PostBillingContractService(
            PublicApi.DTO.v1.BillingContractService billingContractService)
        {
            if (_context.BillingContractServices == null)
            {
                return Problem("Entity set 'AppDbContext.BillingContractServices'  is null.");
            }
            
            var added = _context.BillingContractServices
                .Add(_mapper.Map<App.Domain.BillingContractService>(billingContractService));
            
            var userWhoPays = await _userManager!.FindByIdAsync(User.GetUserId().ToString());

            userWhoPays.Money = userWhoPays.Money - billingContractService.ServiceTotalSum;

            await _context.SaveChangesAsync();

            var mappedback = _mapper.Map<PublicApi.DTO.v1.BillingContractService>(added.Entity);

            return CreatedAtAction("GetBillingContractService", new { id = mappedback.Id },
                mappedback);
        }

        // DELETE: api/BillingContractService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingContractService(Guid id)
        {
            if (_context.BillingContractServices == null)
            {
                return NotFound();
            }

            var billingContractService = await _context.BillingContractServices.FindAsync(id);
            if (billingContractService == null)
            {
                return NotFound();
            }

            _context.BillingContractServices.Remove(billingContractService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingContractServiceExists(Guid id)
        {
            return (_context.BillingContractServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}