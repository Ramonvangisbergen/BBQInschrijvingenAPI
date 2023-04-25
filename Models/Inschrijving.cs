namespace BBQInschrivingen.API.Models
{
    public class Inschrijving
    {
        public int? Id { get; set; } = null;
        public string? TimeStamp { get; set; } = "";
        public string Naam { get; set; } = "";
        public string TicketName
        {
            get
            {
                return $"{TimeStamp}_{Naam}";
            }
        }
        public string? NaamInterne { get; set; } = null;
        public int AantalPersonen { get; set; } = 0;
        public string Reservatie { get; set; } = "17u00";
        public double TotaalBedrag
        {
            get
            {
                var totaal = 0.00;
                if (Formule1 != null && Formule1.Aantal.HasValue)
                {
                    totaal += Formule1.Aantal!.Value * 18.00;
                }

                if (Formule2 != null && Formule2.Aantal.HasValue)
                {
                    totaal += Formule2.Aantal!.Value * 15.00;
                }

                if (Formule3 != null && Formule3.Aantal.HasValue)
                {
                    totaal += Formule3.Aantal!.Value * 18.00;
                }

                if (Formule4 != null && Formule4.Aantal.HasValue)
                {
                    totaal += Formule4.Aantal!.Value * 15.00;
                }

                if (Desserten != null && Desserten.Aantal.HasValue)
                {
                    totaal += Desserten.Aantal!.Value * 1.00;
                }

                return totaal;

            }
        }
        public int TotaalAantalFormules
        {
            get
            {
                return (Formule1 != null && Formule1.Aantal.HasValue ? Formule1.Aantal!.Value : 0) +
                        (Formule2 != null && Formule2.Aantal.HasValue ? Formule2.Aantal!.Value : 0) +
                        (Formule3 != null && Formule3.Aantal.HasValue ? Formule3.Aantal!.Value : 0) +
                        (Formule4 != null && Formule4.Aantal.HasValue ? Formule4.Aantal!.Value : 0);

			}
        }
        public VleesFormule? Formule1 { get; set; } = null;
        public VleesFormule? Formule2 { get; set; } = null;
        public VeggieFormule? Formule3 { get; set; } = null;
        public VeggieFormule? Formule4 { get; set; } = null;
        public Desserten? Desserten { get; set; } = null;
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Naam) ||
                    AantalPersonen == 0 ||
                    AantalPersonen != TotaalAantalFormules ||
                    string.IsNullOrEmpty(Reservatie) ||
					(Desserten != null && Desserten.Aantal.HasValue && Desserten.Aantal!.Value != Desserten.TotaalDesserten))
                {
                    return false;
                }

                if ((Formule1 != null && Formule1.Aantal.HasValue) &&
                    (Formule1.TotaalAantalSnacks != Formule1.Aantal.Value * 3) ||

					(Formule2 != null && Formule2.Aantal.HasValue) &&
					(Formule2.TotaalAantalSnacks != Formule2.Aantal.Value * 2) ||

					(Formule3 != null && Formule3.Aantal.HasValue) &&
					(Formule3.TotaalAantalSnacks != Formule3.Aantal.Value * 3) ||

					(Formule4 != null && Formule4.Aantal.HasValue) &&
					(Formule4.TotaalAantalSnacks != Formule4.Aantal.Value * 2))
                {
                    return false;
                }

				return true;
			}
        }
        public bool? IsBetaald { get; set; } = false;
        public bool? IsAanwezig { get; set; } = false;
    }
   
}


