using BBQInschrivingen.API.Models;

namespace BBQInschrivingen.API.Managers
{
	public interface IInschrijvingManager
	{
		Inschrijving Submit(Inschrijving inschrijving);
		List<Inschrijving> GetAllInschrijvingen();
		void UpdateInschrijving(Inschrijving inschrijving);
		void DeleteInschrijving(int id);
		byte[] GenerateInschrijvingTicket(Inschrijving inschrijving);

	}
}
