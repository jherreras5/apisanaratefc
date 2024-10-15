using Microsoft.AspNetCore.Mvc;
using api_sanarate.Services;
using api_sanarate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<Team>>> Lista()
        {
            var teams = await _service.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult<Team>> Obtener(int id)
        {
            var team = await _service.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound("No se encontró el equipo");
            }
            return Ok(team);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> Crear([FromBody] Team team)
        {
            await _service.AddTeamAsync(team);
            return Ok("Equipo registrado");
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] Team team)
        {
            var respuesta = await _service.UpdateTeamAsync(id, team);
            if (!respuesta)
            {
                return NotFound("No se encontró el equipo para modificar");
            }
            return Ok("Equipo modificado");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var respuesta = await _service.DeleteTeamAsync(id);
            if (!respuesta)
            {
                return NotFound("No se encontró el equipo para eliminar");
            }
            return Ok("Equipo eliminado");
        }
    }
}
