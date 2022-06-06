/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for Foobar
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FooBarController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

      
        /// <summary>
        /// Constructor for foobars
        /// </summary>
        /// <param name="context">Database</param>
        /// <param name="mapper">Mapper to map from domain to public dto</param>
        public FooBarController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FooBar
        /// <summary>
        /// Get All foo bars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.FooBar>>> GetFooBars()
        {
            if (_context.FooBars == null)
            {
                return NotFound();
            }

            return (await _context.FooBars.ToListAsync()).Select(x => _mapper.Map<PublicApi.DTO.v1.FooBar>(x))
                .ToList();
        }

        // GET: api/FooBar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.FooBar>> GetFooBar(Guid id)
        {
            if (_context.FooBars == null)
            {
                return NotFound();
            }

            var fooBar = await _context.FooBars.FindAsync(id);

            if (fooBar == null)
            {
                return NotFound();
            }

            return _mapper.Map<PublicApi.DTO.v1.FooBar>(fooBar);
        }

        // PUT: api/FooBar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFooBar(Guid id, PublicApi.DTO.v1.FooBar fooBar)
        {
            if (id != fooBar.Id)
            {
                return BadRequest();
            }

            _context.Entry(fooBar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FooBarExists(id))
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

        // POST: api/FooBar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.FooBar>> PostFooBar(PublicApi.DTO.v1.FooBar fooBar)
        {
            if (_context.FooBars == null)
            {
                return Problem("Entity set 'AppDbContext.FooBars'  is null.");
            }

            var domainFoo = _mapper.Map<FooBar>(fooBar);

            var dbfoo = _context.FooBars.Add(domainFoo);
            var mappedFoo = _mapper.Map<PublicApi.DTO.v1.FooBar>(dbfoo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFooBar", new { id = mappedFoo.Id }, mappedFoo);
        }

        // DELETE: api/FooBar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFooBar(Guid id)
        {
            if (_context.FooBars == null)
            {
                return NotFound();
            }

            var fooBar = await _context.FooBars.FindAsync(id);
            if (fooBar == null)
            {
                return NotFound();
            }

            _context.FooBars.Remove(fooBar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FooBarExists(Guid id)
        {
            return (_context.FooBars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}*/