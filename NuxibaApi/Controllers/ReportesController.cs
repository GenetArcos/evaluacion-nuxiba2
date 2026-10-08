using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuxibaApi.Data;
using System.Text;

namespace NuxibaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv()
        {
            var users = await _context.Users.Include(u => u.Area).ToListAsync();
            var logins = await _context.Logins.OrderBy(l => l.fecha).ToListAsync();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("NombreDeUsuario,NombreCompleto,Area,TotalHorasTrabajadas");

            foreach (var user in users)
            {
                var userLogins = logins.Where(l => l.User_id == user.id).ToList();
                double totalSegundos = 0;

                for (int i = 0; i < userLogins.Count - 1; i++)
                {
                    if (userLogins[i].TipoMov == 1 && userLogins[i + 1].TipoMov == 0)
                    {
                        totalSegundos += (userLogins[i + 1].fecha - userLogins[i].fecha).TotalSeconds;
                        i++; // Se avanza el índice para no reevaluar el Logout procesado
                    }
                }

                double totalHoras = Math.Round(totalSegundos / 3600.0, 2);
                string nombreCompleto = $"{user.Nombres} {user.ApellidoPaterno} {user.ApellidoMaterno}".Trim();
                string areaNombre = user.Area?.NombreArea ?? "Sin Área";

                csvBuilder.AppendLine($"\"{user.Login}\",\"{nombreCompleto}\",\"{areaNombre}\",{totalHoras}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return File(buffer, "text/csv", "ReporteHorasTrabajadas.csv");
        }
    }
}