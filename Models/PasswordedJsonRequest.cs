namespace BBQInschrivingen.API.Models
{
	public class PasswordedJsonRequest<T>
	{
		public string Password { get; set; }
		public T Model { get; set; }
	}
}
