using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuxibaApi.Data;
using NuxibaApi.Models;

namespace NuxibaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            return await _context.Logins
                .OrderByDescending(l => l.fecha)
                .ToListAsync();
        }

        // POST: api/Logins
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            // 1. Asignar fecha automática si no viene en la petición
            if (login.fecha == default)
            {
                login.fecha = DateTime.Now;
            }

            // 2. Validar que el usuario exista en la tabla ccUsers
            var userExists = await _context.Users.AnyAsync(u => u.id == login.User_id);
            if (!userExists)
            {
                return BadRequest($"El User_id {login.User_id} no existe en la base de datos.");
            }

            // 3. Validar que TipoMov sea únicamente 1 (Login) o 0 (Logout)
            if (login.TipoMov != 0 && login.TipoMov != 1)
            {
                return BadRequest("El TipoMov debe ser 1 (Login) o 0 (Logout).");
            }

            // 4. Obtener el último movimiento registrado para este usuario
            var ultimoMovimiento = await _context.Logins
                .Where(l => l.User_id == login.User_id)
                .OrderByDescending(l => l.fecha)
                .FirstOrDefaultAsync();

            // 5. Aplicar reglas de negocio para alternancia y orden cronológico
            if (ultimoMovimiento != null)
            {
                if (login.TipoMov == 1 && ultimoMovimiento.TipoMov == 1)
                {
                    return BadRequest("El usuario ya tiene una sesión iniciada (login sin logout previo).");
                }
                if (login.TipoMov == 0 && ultimoMovimiento.TipoMov == 0)
                {
                    return BadRequest("El usuario no tiene una sesión activa para cerrar (logout sin login previo).");
                }
                if (login.fecha <= ultimoMovimiento.fecha)
                {
                    return BadRequest($"La fecha del nuevo registro ({login.fecha:yyyy-MM-dd HH:mm:ss}) debe ser posterior al último movimiento ({ultimoMovimiento.fecha:yyyy-MM-dd HH:mm:ss}).");
                }
            }
            else if (login.TipoMov == 0)
            {
                return BadRequest("No se puede registrar un logout inicial sin un login previo.");
            }

            // 6. Guardar en base de datos
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogins), new { id = login.id }, login);
        }

        // PUT: api/logins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.id)
            {
                return BadRequest("El ID proporcionado no coincide con el modelo.");
            }

            var userExists = await _context.Users.AnyAsync(u => u.id == login.User_id);
            if (!userExists)
            {
                return BadRequest($"El User_id {login.User_id} no existe.");
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Logins.AnyAsync(e => e.id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    }
}