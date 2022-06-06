using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContractController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContractController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Contract
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }

            return await _context.Contracts.Select(x => _mapper.Map<Contract>(x)).ToListAsync();
        }
        

        // GET: api/Contract/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(Guid id)
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(x => x.Lessee)
                .Include(x => x.HousingUnit).FirstOrDefaultAsync(x => x.Id == id);

            if (contract == null)
            {
                return NotFound();
            }

            return _mapper.Map<Contract>(contract);
        }
        
        [HttpGet("pendingcontracts")]
        public async Task<ActionResult<IEnumerable<Contract>>> GetPendingContracts()
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }
            
            
            return await _context.Contracts.Include(x => x.Lessee!)
                .Include(x => x.HousingUnit!).Where(x => x.Accepted == false 
                                                         && x.HousingUnit!.OwnerId == User.GetUserId())
                .Select(x => _mapper.Map<Contract>(x)).ToListAsync();
        }
        
        [HttpGet("activecontracts")]
        public async Task<ActionResult<IEnumerable<Contract>>> GetActiveContracts()
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }
            
            
            return await _context.Contracts.Include(x => x.Lessee!)
                .Include(x => x.HousingUnit!).Where(x => x.Accepted == true 
                                                         && x.HousingUnit!.OwnerId == User.GetUserId())
                .Select(x => _mapper.Map<Contract>(x)).ToListAsync();
        }

        // PUT: api/Contract/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(Guid id, Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest();
            }
            try
            {
                var domainContract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == id);
                domainContract!.Accepted = !domainContract.Accepted;

                var domainHousing = await _context.Housings.FirstOrDefaultAsync(x => x.Id == contract.HousingUnitId);
                domainHousing!.IsAvailable = !domainHousing.IsAvailable;
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contract
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'AppDbContext.Contracts'  is null.");
            }

            contract.LesseeId = User.GetUserId();
            contract.Accepted = false;

            var added = _context.Contracts.Add(_mapper.Map<App.Domain.Contract>(contract));
            await _context.SaveChangesAsync();

            var mappedBack = _mapper.Map<Contract>(added.Entity);
            
            
            return CreatedAtAction("GetContract", new { id = mappedBack.Id }, mappedBack);
        }

        // DELETE: api/Contract/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractExists(Guid id)
        {
            return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}