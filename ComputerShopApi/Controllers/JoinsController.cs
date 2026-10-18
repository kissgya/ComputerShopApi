using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinsController : ControllerBase
    {
        private readonly ComputerShopDbContext _context;

        public JoinsController(ComputerShopDbContext context)
        {
            _context = context;
        }

        // 1. Összes Osystem lekérdezése a hozzá tartozó számítógépekkel
        [HttpGet("osystems-with-computers")]
        public async Task<ActionResult<IEnumerable<Osystem>>> GetOsystemsWithComputers()
        {
            // Az .Include() hozza magával a kapcsolódó Computers táblát
            return await _context.Osystems
                .Include(o => o.Computers) // Megjegyzés: ehhez majd a modellbe is be kell kötnünk a navigációs tulajdonságot, ha még nincs
                .ToListAsync();
        }

        // 2. Összes Computer lekérdezése a hozzá tartozó Osystem-mel
        [HttpGet("computers-with-osystems")]
        public async Task<ActionResult<IEnumerable<object>>> GetComputersWithOsystems()
        {
            var result = await _context.Computers
                .Join(
                    _context.Osystems,
                    computer => computer.OsId,
                    osystem => osystem.Id,
                    (computer, osystem) => new
                    {
                        ComputerId = computer.Id,
                        Brand = computer.Brand,
                        Type = computer.Type,
                        Display = computer.Display,
                        Memory = computer.Memory,
                        CreatedTime = computer.CreatedTime,
                        OperatingSystem = osystem.Name,
                        OsVersion = osystem.Version
                    }
                )
                .ToListAsync();

            return Ok(result);
        }
    }
}