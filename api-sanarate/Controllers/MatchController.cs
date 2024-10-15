using Microsoft.AspNetCore.Mvc;
using api_sanarate.Services;
using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _service;

        public MatchController(IMatchService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<Match>>> Lista()
        {
            var matches = await _service.GetAllMatchesAsync();
            return Ok(matches);
        }

        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult<Match>> Obtener(int id)
        {
            var match = await _service.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound("No se encontró el partido");
            }
            return Ok(match);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> Crear([FromBody] Match objeto)
        {
            await _service.AddMatchAsync(objeto);
            return Ok("Partido registrado");
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] Match objeto)
        {
            var respuesta = await _service.UpdateMatchAsync(id, objeto);
            if (!respuesta)
            {
                return NotFound("No se encontró el partido para modificar");
            }
            return Ok("Partido modificado");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var respuesta = await _service.DeleteMatchAsync(id);
            if (!respuesta)
            {
                return NotFound("No se encontró el partido para eliminar");
            }
            return Ok("Partido eliminado");
        }
    }
}
