using BBQInschrivingen.API.Config;
using BBQInschrivingen.API.Managers;
using BBQInschrivingen.API.Data;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace BBQInschrivingen.API.Extensions
{
	public static class BuilderExtensions
	{
		public static void ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
			
			builder.Services.AddControllers();
			builder.ConfigureDependencyInjection();
			builder.ConfigureConfig();
			
		}

		private static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IInschrijvingManager, InschrijvingManager>();
			builder.Services.AddScoped<IUserManager, UserManager>();

			builder.Services.AddScoped<IConfigManager, ConfigManager>();
			builder.Services.AddScoped<IDbContext, DbContext>();
		}
		private static void ConfigureConfig(this WebApplicationBuilder builder)
		{
			string fileServerSettings = "";
			if (builder.Environment.IsDevelopment())
			{
				fileServerSettings = "FileServer_dev";
			}

			if (builder.Environment.IsProduction())
			{
				fileServerSettings = "FileServer";
			}

			builder.Services.Configure<FileServerConfig>(builder.Configuration.GetSection(fileServerSettings)); ;
		}
	}
}
