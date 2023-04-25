using BBQInschrivingen.API.Config;
using BBQInschrivingen.API.Managers;
using BBQInschrivingen.API.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BBQInschrivingen.API.Data
{
	public class DbContext : IDbContext
	{
		private IConfigManager _configManager;
		public DbContext(IConfigManager configManager)
		{
			_configManager = configManager;
		}

		private static object _dbFileLock = new object();
		public void SaveDb(List<Inschrijving> db)
		{
			lock (_dbFileLock)
			{
				bool retry = true;
				while (retry)
				{
					try
					{
						using (StreamWriter writer = new StreamWriter(_configManager.JsonDBFullFileName))
						{
							writer.Write(JsonConvert.SerializeObject(db));
						}

						retry = false;

					}
					catch (IOException)
					{
						retry = true;
					}

				}
				
			}
		}
		public List<Inschrijving> GetDb()
		{
			string json = "";
			lock (_dbFileLock)
			{
				bool retry = true;
				while (retry)
				{
					try
					{
						using (StreamReader reader = new StreamReader(_configManager.JsonDBFullFileName))
						{
							json = reader.ReadToEnd();
						}

						retry = false;
					}
					catch (IOException)
					{

						retry = true;
					}
				}
			}

			var db = JsonConvert.DeserializeObject<List<Inschrijving>>(json);
			if (db == null)
			{
				return new List<Inschrijving>();
			}
			else
			{
				return db;
			}
		}

	}
}
