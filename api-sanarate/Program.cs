using api_sanarate.Models;
using api_sanarate.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la conexi�n a MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31)), // Cambiar a la versi�n correcta
        mySqlOptions => mySqlOptions.EnableRetryOnFailure() // Habilitar reintentos autom�ticos
    ));

// Registrar los servicios
builder.Services.AddScoped<IHistoriaService, HistoriaService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMatchService, MatchService>();

// Agregar controladores
builder.Services.AddControllers();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://paginawebsanarate.web.app/")  // Cambia esta URL seg�n tu frontend
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

// Usar CORS antes de la autorizaci�n
app.UseCors("AllowSpecificOrigin");

// Habilitar el servidor de archivos est�ticos para la carpeta "uploads"
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
