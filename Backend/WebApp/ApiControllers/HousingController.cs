using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Enum;
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
    public class HousingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HousingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Housing
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Housing>>> GetHousings()
        {
            if (_context.Housings == null)
            {
                return NotFound();
            }

            return await _context.Housings.Include(x => x.Owner).Select(x => _mapper.Map<Housing>(x)).ToListAsync();
        }
        [HttpGet("availablehousings")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Housing>>> GetAvailableHousings()
        {
            if (_context.Housings == null)
            {
                return NotFound();
            }

            return await _context.Housings.Include(x => x.Owner)
                .Where(x => x.IsAvailable)
                .Select(x => _mapper.Map<Housing>(x)).ToListAsync();
        }

        [HttpGet("myhousings")]
        public async Task<ActionResult<IEnumerable<Housing>>> GetUserOwnedHousings()
        {
            if (_context.Housings == null)
            {
                return NotFound();
            }

            return await _context.Housings.Where(x => x.OwnerId == User.GetUserId())
                .Select(x => _mapper.Map<Housing>(x))
                .ToListAsync();
        }

        

        // GET: api/Housing/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Housing>> GetHousing(Guid id)
        {
            if (_context.Housings == null)
            {
                return NotFound();
            }

            var housing = await _context.Housings.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == id);

            if (housing == null)
            {
                return NotFound();
            }

            return _mapper.Map<Housing>(housing);
        }

        // PUT: api/Housing/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHousing(Guid id, Housing housing)
        {
            if (id != housing.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Housings.Update(_mapper.Map<App.Domain.Housing>(housing));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HousingExists(id))
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

        // POST: api/Housing
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Housing>> PostHousing(HousingPost housing)
        {
            if (_context.Housings == null)
            {
                return Problem("Entity set 'AppDbContext.Housings'  is null.");
            }

            var domainHousing = new App.Domain.Housing()
            {
                SquareMeters = housing.SquareMeters,
                RoomsCount = housing.RoomsCount,
                Description = housing.Description,
                Location = housing.Location,
                Amenities = housing.Amenities,
                IsAvailable = housing.IsAvailable,
                PictureUrl = housing.PictureUrl,
                Price = housing.Price,
                DealType = housing.DealType == 0 ? EActionType.Sale : EActionType.Rent,
                OwnerId = User.GetUserId()
            };


            var added = _context.Housings.Add(domainHousing);

            await _context.SaveChangesAsync();
            var mappedBack = _mapper.Map<Housing>(added.Entity);

            return CreatedAtAction("GetHousing", new { id = mappedBack.Id }, mappedBack);
        }

        // DELETE: api/Housing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHousing(Guid id)
        {
            if (_context.Housings == null)
            {
                return NotFound();
            }

            var housing = await _context.Housings.FindAsync(id);
            if (housing == null)
            {
                return NotFound();
            }

            _context.Housings.Remove(housing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HousingExists(Guid id)
        {
            return (_context.Housings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}