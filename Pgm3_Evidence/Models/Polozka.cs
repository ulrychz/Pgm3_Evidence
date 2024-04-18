namespace Pgm3_Evidence.Models
{
    public class Polozka
    {
        public Polozka() 
        {
            Datum = DateOnly.FromDateTime(DateTime.Now);
        }
        public Polozka (DateOnly datum, double naklady, double vynosy)
        {
            Datum = datum;
            Naklady = naklady;
            Vynosy = vynosy;
        }
        public double Naklady { get; set; }
        public double Vynosy { get; set; }
        public DateOnly Datum { get; set; } 

        public double Zisk => Vynosy - Naklady;
    }
}
