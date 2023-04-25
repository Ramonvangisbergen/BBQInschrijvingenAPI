namespace BBQInschrivingen.API.Managers
{
	public interface IUserManager
	{
		bool ValidatePassword(string password);
	}
}
