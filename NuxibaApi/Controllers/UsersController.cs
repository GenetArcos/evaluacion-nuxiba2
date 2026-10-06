using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuxibaApi.Data;
using System.Text;

namespace NuxibaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Area)
                .Select(u => new
                {
                    u.IdUser,
                    u.Username,
                    NombreCompleto = $"{u.FirstName} {u.LastName}",
                    u.Email,
                    Area = u.Area != null ? u.Area.NombreArea : "Sin Área"
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/users/export-csv
        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv()
        {
            var users = await _context.Users
                .Include(u => u.Area)
                .Select(u => new
                {
                    u.IdUser,
                    u.Username,
                    NombreCompleto = $"{u.FirstName} {u.LastName}",
                    u.Email,
                    Area = u.Area != null ? u.Area.NombreArea : "Sin Área"
                })
                .ToListAsync();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("IdUser,Username,NombreCompleto,Email,Area");

            foreach (var user in users)
            {
                csvBuilder.AppendLine($"{user.IdUser},{user.Username},{user.NombreCompleto},{user.Email},{user.Area}");
            }

            var bytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return File(bytes, "text/csv", "ReporteUsuarios.csv");
        }
    }
}