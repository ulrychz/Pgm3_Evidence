using Pgm3_Evidence.Models;

namespace Pgm3_Evidence.Pages
{
    public partial class EvidenceV1
    {
        public Polozka Polozka { get; set; } = new Polozka();

        public List<Models.Polozka> Polozky { get; set; } = new List<Polozka>();
        public void Pridat()
        {
            Polozky.Add(new Polozka(Polozka.Datum, Polozka.Naklady, Polozka.Vynosy));
            
            RDataGrid.RefreshDataAsync();

        }

    }


}
