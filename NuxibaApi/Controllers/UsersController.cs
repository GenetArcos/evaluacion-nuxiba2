using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuxibaApi.Data;

namespace NuxibaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Area)
                .Select(u => new
                {
                    u.id,
                    u.Login,
                    NombreCompleto = $"{u.Nombres} {u.ApellidoPaterno} {u.ApellidoMaterno}".Trim(),
                    Area = u.Area != null ? u.Area.NombreArea : "Sin Área"
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}