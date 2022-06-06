using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContractServiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContractServiceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ContractService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractService>>> GetContractServices()
        {
            if (_context.ContractServices == null)
            {
                return NotFound();
            }

            return await _context.ContractServices.Select(x => _mapper.Map<ContractService>(x)).ToListAsync();
        }
        
        [HttpGet("contract/{id}")]
        public async Task<ActionResult<IEnumerable<ContractService>>> GetContractServicesByContractId(Guid id)
        {
            if (_context.ContractServices == null)
            {
                
                return NotFound();
            }

            var contractServices = await _context.ContractServices
                .Include(x => x.Service)
                .Where(x => x.ContractId == id).ToListAsync();

            return contractServices.Select(x => _mapper.Map<ContractService>(x)).ToList();
        }

        // GET: api/ContractService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractService>> GetContractService(Guid id)
        {
            if (_context.ContractServices == null)
            {
                return NotFound();
            }

            var contractService = await _context.ContractServices.FindAsync(id);

            if (contractService == null)
            {
                return NotFound();
            }

            return _mapper.Map<ContractService>(contractService);
        }

        // PUT: api/ContractService/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractService(Guid id, ContractService contractService)
        {
            if (id != contractService.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _context.ContractServices.Update(_mapper.Map<App.Domain.ContractService>(contractService));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractServiceExists(id))
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

        // POST: api/ContractService
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractService>> PostContractService(ContractService contractService)
        {
            if (_context.ContractServices == null)
            {
                return Problem("Entity set 'AppDbContext.ContractServices'  is null.");
            }

            var added = _context.ContractServices.Add(_mapper.Map<App.Domain.ContractService>(contractService));
            await _context.SaveChangesAsync();

            var mappedBack = _mapper.Map<ContractService>(added.Entity);

            return CreatedAtAction("GetContractService", new { id = mappedBack.Id }, mappedBack);
        }

        // DELETE: api/ContractService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractService(Guid id)
        {
            if (_context.ContractServices == null)
            {
                return NotFound();
            }

            var contractService = await _context.ContractServices.FindAsync(id);
            if (contractService == null)
            {
                return NotFound();
            }

            _context.ContractServices.Remove(contractService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractServiceExists(Guid id)
        {
            return (_context.ContractServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}