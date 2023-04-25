using BBQInschrivingen.API.Config;
using Microsoft.Extensions.Options;

namespace BBQInschrivingen.API.Managers
{
	public class ConfigManager : IConfigManager
	{
		private FileServerConfig _fileServerConfig;

		private readonly IWebHostEnvironment _webHostEnvironment;
		public ConfigManager(IOptions<FileServerConfig> fileServerConfig, IWebHostEnvironment webHostEnvironment)
		{
			_fileServerConfig = fileServerConfig.Value;
			_webHostEnvironment = webHostEnvironment;
		}

		public string JsonDBFullFileName
		{
			get
			{
				return Path.Combine(_webHostEnvironment.ContentRootPath, _fileServerConfig.DatabaseFolder, _fileServerConfig.DBFileName);
			}
			
		}
		public string AdminPassword { get { return _fileServerConfig.AdminPassword; } }

	}
}
