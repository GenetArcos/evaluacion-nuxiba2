using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NuxibaApi.Models
{
    [Table("ccRIACat_Areas")]
    public class Area
    {
        [Key]
        public int IdArea { get; set; }
        public string? NombreArea { get; set; }
    }
}