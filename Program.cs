using BBQInschrivingen.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();

var app = builder.Build();

//Configure the app to serve static files and enable default file mapping. 
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
	EnableDirectoryBrowsing = true,
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
