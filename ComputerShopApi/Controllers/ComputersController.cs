using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly ComputerShopDbContext _context;

        public ComputersController(ComputerShopDbContext context)
        {
            _context = context;
        }

        // GET: api/Computers (Összes számítógép lekérdezése)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Computer>>> GetComputers()
        {
            return await _context.Computers.ToListAsync();
        }

        // GET: api/Computers/5 (Egyetlen számítógép lekérdezése ID alapján)
        [HttpGet("{id}")]
        public async Task<ActionResult<Computer>> GetComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);

            if (computer == null)
            {
                return NotFound();
            }

            return computer;
        }

        // POST: api/Computers (Új számítógép létrehozása)
        [HttpPost]
        public async Task<ActionResult<Computer>> PostComputer(Computer computer)
        {
            computer.CreatedTime = DateTime.Now; // Beállítjuk a létrehozás idejét

            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComputer), new { id = computer.Id }, computer);
        }

        // PUT: api/Computers/5 (Meglévő számítógép módosítása)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputer(int id, Computer computer)
        {
            if (id != computer.Id)
            {
                return BadRequest();
            }

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Computers.Any(e => e.Id == id))
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

        // DELETE: api/Computers/5 (Számítógép törlése ID alapján)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}