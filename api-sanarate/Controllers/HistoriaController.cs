using Microsoft.AspNetCore.Mvc;
using api_sanarate.Services;
using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriaController : ControllerBase
    {
        private readonly IHistoriaService _service;

        // Constructor para la inyección de dependencias del servicio
        public HistoriaController(IHistoriaService service)
        {
            _service = service;
        }

        // Obtener todas las historias
        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<Historia>>> Lista()
        {
            var historias = await _service.GetAllHistoriasAsync();
            return Ok(historias);
        }

        // Obtener una historia por ID
        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult<Historia>> Obtener(int id)
        {
            var historia = await _service.GetHistoriaByIdAsync(id);
            if (historia == null)
            {
                return NotFound("No se encontró la historia");
            }
            return Ok(historia);
        }

        // Crear una nueva historia
        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> Crear([FromBody] Historia objeto)
        {
            await _service.AddHistoriaAsync(objeto);
            return Ok("Historia registrada");
        }

        // Editar una historia existente
        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] Historia objeto)
        {
            var respuesta = await _service.UpdateHistoriaAsync(id, objeto);
            if (!respuesta)
            {
                return NotFound("No se encontró la historia para modificar");
            }
            return Ok("Historia modificada");
        }

        // Eliminar una historia por ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var respuesta = await _service.DeleteHistoriaAsync(id);
            if (!respuesta)
            {
                return NotFound("No se encontró la historia para eliminar");
            }
            return Ok("Historia eliminada");
        }
    }
}
