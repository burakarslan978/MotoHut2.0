namespace MotoHut2._0.Models
{
    public class HuurderMotorViewModel
    {
        public int HuurderMotorId { get; set; }
        public int MotorId { get; set; }
        public int HuurderId { get; set; }
        public DateTime OphaalDatum { get; set; }
        public DateTime InleverDatum { get; set; }
        public bool IsGeaccepteerd { get; set; }
        public bool IsGeweigerd { get; set; }
    }
}
