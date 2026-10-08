using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NuxibaApi.Models
{
    [Table("ccUsers")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string? Login { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? IdArea { get; set; }

        [ForeignKey("IdArea")]
        public Area? Area { get; set; }
    }
}