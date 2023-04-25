using BBQInschrivingen.API.Models;

namespace BBQInschrivingen.API.Data
{
	public interface IDbContext
	{
		void SaveDb(List<Inschrijving> db);
		List<Inschrijving> GetDb();
	}
}
