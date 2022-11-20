namespace MotoHut2._0.Models
{
    public class MotorViewModel
    {
        public int MotorId { get; set; }
        public int VerhuurderId { get; set; }
        public int Bouwjaar { get; set; }
        public int Prijs { get; set; }
        public string Model { get; set; }
        public bool Huurbaar { get; set; }
    }
}
