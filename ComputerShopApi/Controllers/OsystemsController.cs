using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsystemsController : ControllerBase
    {
        private readonly ComputerShopDbContext _context;

        // Konstruktor, ami megkapja az adatbázis-kapcsolatot
        public OsystemsController(ComputerShopDbContext context)
        {
            _context = context;
        }

        // 1. READ: Összes operációs rendszer lekérdezése
        // GET: api/Osystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Osystem>>> GetOsystems()
        {
            return await _context.Osystems.ToListAsync();
        }

        // GET: api/Osystems/5 (Egyetlen operációs rendszer lekérdezése ID alapján)
        [HttpGet("{id}")]
        public async Task<ActionResult<Osystem>> GetOsystem(Guid id)
        {
            var osystem = await _context.Osystems.FindAsync(id);

            if (osystem == null)
            {
                return NotFound(); // 404-es hiba, ha nincs ilyen ID
            }

            return osystem;
        }

        // POST: api/Osystems (Új operációs rendszer létrehozása)
        [HttpPost]
        public async Task<ActionResult<Osystem>> PostOsystem(Osystem osystem)
        {
            _context.Osystems.Add(osystem);
            await _context.SaveChangesAsync();

            // Sikeres mentés után visszaküldi a 201-es kódot és a létrejött adatot
            return CreatedAtAction(nameof(GetOsystem), new { id = osystem.Id }, osystem);
        }

        // PUT: api/Osystems/5 (Meglévő operációs rendszer módosítása)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOsystem(Guid id, Osystem osystem)
        {
            if (id != osystem.Id)
            {
                return BadRequest(); // 400-as hiba, ha az URL-ben lévő ID nem egyezik a küldött objektuméval
            }

            // Beállítjuk a frissítés idejét automatikusan
            osystem.UpdateTime = DateTime.Now;

            _context.Entry(osystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Osystems.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204-es kód sikeres módosítás esetén (nincs visszatérési érték)
        }

        // DELETE: api/Osystems/5 (Operációs rendszer törlése ID alapján)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOsystem(Guid id)
        {
            var osystem = await _context.Osystems.FindAsync(id);
            if (osystem == null)
            {
                return NotFound(); // 404, ha nem létezik
            }

            _context.Osystems.Remove(osystem);
            await _context.SaveChangesAsync();

            return NoContent(); // 204-es sikeres törlés
        }
    }
}