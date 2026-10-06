namespace NuxibaApi.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int? IdArea { get; set; }

        public Area? Area { get; set; }
    }
}