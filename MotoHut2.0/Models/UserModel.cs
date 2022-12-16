namespace MotoHut2._0.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? Naam { get; set; }
        public string? Email { get; set; }
        public int VerhuurderId { get; set; }
        public int HuurderId { get; set; }
        public DateTime Geboortedatum { get; set; }
        public int Leeftijd => DateTime.Now.Subtract(Geboortedatum).Days / 365;
    }
}
