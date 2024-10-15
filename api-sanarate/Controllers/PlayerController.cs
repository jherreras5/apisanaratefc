using Microsoft.AspNetCore.Mvc;
using api_sanarate.Services;
using api_sanarate.Models;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<Player>>> Lista()
        {
            var players = await _service.GetAllPlayersAsync();
            return Ok(players);
        }

        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult<Player>> Obtener(int id)
        {
            var player = await _service.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound("No se encontró el jugador");
            }
            return Ok(player);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> Crear([FromBody] Player player)
        {
            await _service.AddPlayerAsync(player);
            return Ok("Jugador registrado");
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] Player player)
        {
            var respuesta = await _service.UpdatePlayerAsync(id, player);
            if (!respuesta)
            {
                return NotFound("No se encontró el jugador para modificar");
            }
            return Ok("Jugador modificado");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var respuesta = await _service.DeletePlayerAsync(id);
            if (!respuesta)
            {
                return NotFound("No se encontró el jugador para eliminar");
            }
            return Ok("Jugador eliminado");
        }
    }
}
