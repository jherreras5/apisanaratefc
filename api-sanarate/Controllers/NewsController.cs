using Microsoft.AspNetCore.Mvc;
using api_sanarate.Services;
using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<News>>> Lista()
        {
            var newsList = await _service.GetAllNewsAsync();
            return Ok(newsList);
        }

        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult<News>> Obtener(int id)
        {
            var newsItem = await _service.GetNewsByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound("Noticia no encontrada");
            }
            return Ok(newsItem);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> Crear([FromBody] News news)
        {
            await _service.AddNewsAsync(news);
            return Ok("Noticia registrada");
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] News news)
        {
            var respuesta = await _service.UpdateNewsAsync(id, news);
            if (!respuesta)
            {
                return NotFound("No se encontró la noticia para modificar");
            }
            return Ok("Noticia modificada");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var respuesta = await _service.DeleteNewsAsync(id);
            if (!respuesta)
            {
                return NotFound("No se encontró la noticia para eliminar");
            }
            return Ok("Noticia eliminada");
        }
    }
}
