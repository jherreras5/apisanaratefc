using api_sanarate.Models;
using api_sanarate.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Permite escuchar en todas las interfaces

// Configuración de la conexión a MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31)), // Cambiar a la versión correcta
        mySqlOptions => mySqlOptions.EnableRetryOnFailure() // Habilitar reintentos automáticos
    ));

// Registrar los servicios
builder.Services.AddScoped<IHistoriaService, HistoriaService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMatchService, MatchService>();

// Agregar controladores
builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()  // Permitir solicitudes desde cualquier origen
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });

});
// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

// Usar CORS antes de la autorización
app.UseCors("AllowSpecificOrigin");

// Habilitar el servidor de archivos estáticos para la carpeta "uploads"
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});
string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
