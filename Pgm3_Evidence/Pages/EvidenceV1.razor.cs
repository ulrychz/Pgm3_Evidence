using Pgm3_Evidence.Models;
using Radzen;
using System.Globalization;

namespace Pgm3_Evidence.Pages
{
    public partial class EvidenceV1
    {
        public EvidenceV1() 
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("cs-CZ");
        }
        public Polozka Polozka { get; set; } = new Polozka();

        public List<Models.Polozka> Polozky { get; set; } = new List<Polozka>();

        //public List<Models.Polozka> PolozkySeskupene => Polozky.OrderBy(x => x.Datum).GroupBy(y=>y.Datum)
        //    .Select(g=>new Polozka(new DateOnly(g.Key.Year, g.Key.Month, g.Key.Day),g.Sum(y=>y.Naklady),
        //        g.Sum(x=>x.Vynosy))).ToList();
        public List<Models.Polozka> PolozkySeskupene => Polozky.OrderBy(x => x.Datum)
            .GroupBy(y => new {y.Datum.Year, y.Datum.Month})
            .Select(g => new Polozka(new DateOnly(g.Key.Year, g.Key.Month, 1), g.Sum(y => y.Naklady),
               g.Sum(x => x.Vynosy))).ToList();
        public List<Models.Polozka> PolozkySeskupeneVLinq => (from p in Polozky
            group p by new {p.Datum.Year, p.Datum.Month } into g
            orderby g.Key.Year, g.Key.Month
            select new Polozka(new DateOnly(g.Key.Year, g.Key.Month, 1), g.Sum(y => y.Naklady),
            g.Sum(x => x.Vynosy))
            ).ToList();

        public List<Models.Polozka> PolozkySeskupeneZiskKladny => Polozky.Where(x=>x.Zisk>=0).ToList();
        public void Pridat()
        {
            Polozky.Add(new Polozka(Polozka.Datum, Polozka.Naklady, Polozka.Vynosy));
            
            RDataGrid.RefreshDataAsync();
            RDataGridGroup.RefreshDataAsync();

        }
        public async Task SmazatZaznam(Polozka polozka)
        {
            string zprava = $"Chcete smazat {polozka.Datum} - Zisk: {polozka.Zisk} z vašeho seznamu";
            bool? result = await DialogService.Confirm(zprava, "Smazat", new ConfirmOptions() { OkButtonText = "Ano", CancelButtonText = "Ne" });
            if (result.HasValue && result == true)
            {
                Polozky.Remove(polozka);
                RDataGrid?.RefreshDataAsync();
                RDataGridGroup?.RefreshDataAsync();
            }
        }
        public bool IsEditace { get; set; }
        public void EditujZaznam(Polozka polozka)
        {
            IsEditace = true;
            Polozka = polozka;
        }
        public void UkociEditaci()
        {
            IsEditace = false;
            Polozka = new Polozka();
        }
    }


}
