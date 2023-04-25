namespace BBQInschrivingen.API.Managers
{
    public interface IConfigManager
    {
		string JsonDBFullFileName { get; }
		string AdminPassword { get; }
	}
}
