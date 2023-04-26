using BBQInschrivingen.API.Data;
using BBQInschrivingen.API.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BBQInschrivingen.API.Managers
{
	public class InschrijvingManager : IInschrijvingManager
	{
		private IConfigManager _configManager;
		private IDbContext _dbContext;
		private readonly IConverter _converter;
		public InschrijvingManager(IConfigManager configManager, IDbContext dbContext, IConverter converter)
		{
			_configManager = configManager;
			_dbContext = dbContext;
			_converter = converter;
		}
		public Inschrijving Submit(Inschrijving inschrijving)
		{
			
			if (!inschrijving.IsValid)
			{
				throw new Exception("Invalid 'inschrijving' model");
			}

			inschrijving.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");

			var db = _dbContext.GetDb();
			if (db == null)
			{
				db = new List<Inschrijving>();
			}

			var maxId = db.Max(i => i.Id);
			inschrijving.Id = maxId.HasValue ? maxId.Value + 1 : 1;
			db.Add(inschrijving);

			_dbContext.SaveDb(db);

			return inschrijving;
		}
		public List<Inschrijving> GetAllInschrijvingen()
		{
			return _dbContext.GetDb();
		}

		public void UpdateInschrijving(Inschrijving inschrijving)
		{
			var inschrijvingen = _dbContext.GetDb();
			var current = inschrijvingen.Find(i => i.Id == inschrijving.Id);
			inschrijvingen.Remove(current);

			inschrijvingen.Add(inschrijving);

			_dbContext.SaveDb(inschrijvingen);
		}

		public void DeleteInschrijving(int id)
		{
			var inschrijvingen = _dbContext.GetDb().Where(i => i.Id != id).ToList();
			_dbContext.SaveDb(inschrijvingen);
		}
		public byte[] GenerateInschrijvingTicket(Inschrijving inschrijving)
		{
			var ticketHtml = "<p>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += $"<b>Inschrijvingsnummer:</b> {inschrijving.Id}<br/>";
			ticketHtml += $"<b>Naam:</b> {inschrijving.Naam}<br/>";

			ticketHtml += $"<b>Naam interne:</b> {inschrijving.NaamInterne}<br/>";

			ticketHtml += $"<b>Aantal personen:</b> {inschrijving.AantalPersonen}<br/>";
			ticketHtml += $"<b>Tijdstip reservering:</b> {inschrijving.Reservatie}<br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += "<b>BESTELLING:</b> <br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";

			if (inschrijving.Formule1 != null && inschrijving.Formule1.Aantal.HasValue && inschrijving.Formule1.Aantal > 0)
			{
				ticketHtml += $"FORMULE 1({inschrijving.Formule1.Aantal.Value} x &#8364;18,00 = &#8364;{inschrijving.Formule1.TotaalFormule18Euro},00)<br/>";

				if (inschrijving.Formule1.Hamburgers.HasValue && inschrijving.Formule1.Hamburgers.Value > 0)
				{
					ticketHtml += $"--hamburgers: {inschrijving.Formule1.Hamburgers.Value}<br/>";
				}

				if (inschrijving.Formule1.Kipfilets.HasValue && inschrijving.Formule1.Kipfilets.Value > 0)
				{
					ticketHtml += $"--kipfilets: {inschrijving.Formule1.Kipfilets.Value}<br/>";
				}

				if (inschrijving.Formule1.Worsten.HasValue && inschrijving.Formule1.Worsten.Value > 0)
				{
					ticketHtml += $"--worsten: {inschrijving.Formule1.Worsten.Value}<br/>";
				}

				if (inschrijving.Formule1.Sates.HasValue && inschrijving.Formule1.Sates.Value > 0)
				{
					ticketHtml += $"--sat&eacute;s: {inschrijving.Formule1.Sates.Value}<br/>";
				}

				ticketHtml += @"<br/>";
			}

			if (inschrijving.Formule2 != null && inschrijving.Formule2.Aantal.HasValue && inschrijving.Formule2.Aantal > 0)
			{
				ticketHtml += $"FORMULE 2({inschrijving.Formule2.Aantal.Value} x &#8364;15,00 = &#8364;{inschrijving.Formule2.TotaalFormule15Euro},00)<br/>";

				if (inschrijving.Formule2.Hamburgers.HasValue && inschrijving.Formule2.Hamburgers.Value > 0)
				{
					ticketHtml += $"--hamburgers: {inschrijving.Formule2.Hamburgers.Value}<br/>";
				}

				if (inschrijving.Formule2.Kipfilets.HasValue && inschrijving.Formule2.Kipfilets.Value > 0)
				{
					ticketHtml += $"--kipfilets: {inschrijving.Formule2.Kipfilets.Value}<br/>";
				}

				if (inschrijving.Formule2.Worsten.HasValue && inschrijving.Formule2.Worsten.Value > 0)
				{
					ticketHtml += $"--worsten: {inschrijving.Formule2.Worsten.Value}<br/>";
				}

				if (inschrijving.Formule2.Sates.HasValue && inschrijving.Formule2.Sates.Value > 0)
				{
					ticketHtml += $"--sat&eacute;s: {inschrijving.Formule2.Sates.Value}<br/>";
				}

				ticketHtml += @"<br/>";
			}

			if (inschrijving.Formule3 != null && inschrijving.Formule3.Aantal.HasValue && inschrijving.Formule3.Aantal > 0)
			{
				ticketHtml += $"FORMULE 3({inschrijving.Formule3.Aantal!.Value} x &#8364;18,00 = &#8364;{inschrijving.Formule3.TotaalFormule18Euro},00)<br/>";

				if (inschrijving.Formule3.Hamburgers.HasValue && inschrijving.Formule3.Hamburgers.Value > 0)
				{
					ticketHtml += $"--veggie hamburgers: {inschrijving.Formule3.Hamburgers.Value}<br/>";
				}

				if (inschrijving.Formule3.Worsten.HasValue && inschrijving.Formule3.Worsten.Value > 0)
				{
					ticketHtml += $"--veggie worsten: {inschrijving.Formule3.Worsten.Value}<br/>";
				}

				if (inschrijving.Formule3.Balletjes.HasValue && inschrijving.Formule3.Balletjes.Value > 0)
				{
					ticketHtml += $"--veggie balletjes: {inschrijving.Formule3.Balletjes.Value}<br/>";
				}

				ticketHtml += @"<br/>";
			}

			if (inschrijving.Formule4 != null && inschrijving.Formule4.Aantal.HasValue && inschrijving.Formule4.Aantal > 0)
			{
				ticketHtml += $"FORMULE 4({inschrijving.Formule4.Aantal!.Value} x &#8364;15,00 = &#8364;${inschrijving.Formule4.TotaalFormule15Euro},00)<br/>";

				if (inschrijving.Formule4.Hamburgers.HasValue && inschrijving.Formule4.Hamburgers.Value > 0)
				{
					ticketHtml += $"--veggie hamburgers: {inschrijving.Formule4.Hamburgers.Value}<br/>";
				}

				if (inschrijving.Formule4.Worsten.HasValue && inschrijving.Formule4.Worsten.Value > 0)
				{
					ticketHtml += $"--veggie worsten: {inschrijving.Formule4.Worsten.Value}<br/>";
				}

				if (inschrijving.Formule4.Balletjes.HasValue && inschrijving.Formule4.Balletjes.Value > 0)
				{
					ticketHtml += $"--veggie balletjes: {inschrijving.Formule4.Balletjes.Value}<br/>";
				}

				ticketHtml += @"<br/>";
			}

			var desserten = inschrijving.Desserten != null ? inschrijving.Desserten.TotaalDesserten : 0;
			ticketHtml += $"DESSERTEN({desserten} x &#8364;1,00 = &#8364;{desserten},00)<br/>";


			if (desserten > 0 && inschrijving.Desserten!.Chocomousses.HasValue && inschrijving.Desserten!.Chocomousses.Value > 0)
			{
				ticketHtml += $"--chocomousse: {inschrijving.Desserten!.Chocomousses.Value}<br/>";
			}

			if (desserten > 0 && inschrijving.Desserten!.Rijstpappen.HasValue && inschrijving.Desserten!.Rijstpappen.Value > 0)
			{
				ticketHtml += $"--rijstpap: {inschrijving.Desserten!.Rijstpappen.Value}<br/>";
			}

			if (desserten > 0 && inschrijving.Desserten!.DameBlanches.HasValue && inschrijving.Desserten!.DameBlanches.Value > 0)
			{
				ticketHtml += $"--dame blanche: {inschrijving.Desserten!.DameBlanches.Value}<br/>";
			}


			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += $"<b>TOTAAL TE BETALEN:</b> &#8364;{inschrijving.TotaalBedrag}<br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += @"<b>Begunstigde:</b> Instituut van het Heilig-Graf <br/>";
			ticketHtml += @"<b>Rekeningnummer:</b> BE18 7330 3207 1765 <br/>";
			ticketHtml += @"<b>Vrije vermelding:</b> BBQ internaat + naam interne (indien geen link met de interne gewoon de eigen naam) <br/>";
			ticketHtml += @"<b>Te betalen voor:</b> 23 mei 2023 <br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";
			ticketHtml += "---------------------------------------------------------------------------------------------------------------<br/>";

			ticketHtml += @"</p>";


			HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
			{
				Objects = 
				{ 
					new ObjectSettings()
					{
						HtmlContent = ticketHtml
					} 
				}
			};

			var pdfBuffer = _converter.Convert(htmlToPdfDocument);
			return pdfBuffer;
		}
	}
}
