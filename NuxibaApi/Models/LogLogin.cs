namespace NuxibaApi.Models
{
    public class LogLogin
    {
        public int IdLog { get; set; }
        public int IdUser { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }

        public User? User { get; set; }
    }
}