using BBQInschrivingen.API.Config;

namespace BBQInschrivingen.API.Managers
{
	public class UserManager : IUserManager
	{
		private IConfigManager _configManager;
		public UserManager(IConfigManager configManager)
		{
			_configManager = configManager;
		}

		public bool ValidatePassword(string password)
		{
			return password.Equals(_configManager.AdminPassword);
		}
	}
}
