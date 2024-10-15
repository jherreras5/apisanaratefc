using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadsPath;

        public UploadController()
        {
            // Establece la ruta de la carpeta donde se guardarán los archivos subidos
            _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            // Si la carpeta no existe, créala
            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No se ha proporcionado ningún archivo");

            // Crear la ruta completa donde se almacenará el archivo
            var filePath = Path.Combine(_uploadsPath, file.FileName);

            // Guardar el archivo en la carpeta
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }
    }
}
