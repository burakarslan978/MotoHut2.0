namespace MotoHut2._0.Models
{
    public class HuurderMotorViewModel
    {
        public int HuurderMotorId { get; set; }
        public int MotorId { get; set; }
        public int HuurderId { get; set; }
        public int HuurderLeeftijd { get; set; }
        public string HuurderNaam { get; set; }
        public DateTime OphaalDatum { get; set; }
        public DateTime InleverDatum { get; set; }
        public bool? IsGeaccepteerd { get; set; }
        public int Prijs { get; set; }
        public int Dagen { get; set; }
        public int TotaalPrijs { get; set; }
    }
}
