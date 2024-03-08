using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homes_api.Data;
using homes_api.Models;
using homes_api.Services.Interfaces;
using System.Runtime.InteropServices;

namespace homes_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHomeService _homeService;

        public HomesController(ApplicationDbContext context, IHomeService homeService)
        {
            _context = context;
            _homeService = homeService;
        }

        // GET: api/Homes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomes()
        {
            return await _homeService.GetAllHomesAsync();
        }

        // GET: api/Homes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Home>> GetHome(int id)
        {
            Home home = await _homeService.GetHomeByIdAsync(id);


            if (home == null)
            {
                return NotFound();
            }

            return Ok(home);
        }

        // PUT: api/Homes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome(int id, Home home)
        {

            bool success = await _homeService.UpdateHomeAsync(id, home);

            if (!success)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }

        }

        // POST: api/Homes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostHome(Home home)
        {
            bool success = await _homeService.AddNewHomeAsync(home);

            return success == false ? StatusCode(400) : CreatedAtAction("GetHome", new { id = home.Id }, home);
        }

        // DELETE: api/Homes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {

            bool success =  await _homeService.RemoveHomeAsync(id);

            return success == false ? NotFound() : NoContent();
        }

        //private bool HomeExists(int id)
        //{
        //    return _context.Homes.Any(e => e.Id == id);
        //}


    }
}
