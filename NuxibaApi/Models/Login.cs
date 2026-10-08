namespace NuxibaApi.Models
{
    public class Login
    {
        public int id { get; set; }
        public int User_id { get; set; }
        public int Extension { get; set; }
        public int TipoMov { get; set; } // 1 = Login, 0 = Logout
        public DateTime fecha { get; set; } = DateTime.Now;
    }
}