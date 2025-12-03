using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecyRoute.Modelos;
using RecyRoute.Repositories;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;

        public RolController(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }
        [HttpGet("ObtenerRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerRoles()
        {
            try
            {
                var roles = await _rolRepository.ObtenerRoles();

                if (roles == null || !roles.Any())
                    return NotFound("No se encontraron roles registrados.");

                return Ok(roles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los roles.");
            }
        }

        // -------------------- GET: api/rol/ObtenerRolPorId/{idrol} --------------------
        [HttpGet("ObtenerRolPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerRolPorId(Guid idrol)
        {
            try
            {
                var rol = await _rolRepository.ObtenerRol(idrol);

                if (rol == null)
                    return NotFound("No se encontró el rol solicitado.");

                return Ok(rol);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el rol.");
            }
        }

        // -------------------- POST: api/rol/CrearRol --------------------
        [HttpPost("CrearRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearRol([FromBody] Rol rol)
        {
            try
            {
                if (rol == null)
                    return BadRequest("Los datos del rol no pueden ser nulos.");

                var nuevoRol = await _rolRepository.CrearRol(rol);
                return Ok(nuevoRol);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el rol.");
            }
        }

        // -------------------- PUT: api/rol/ActualizarRol --------------------
        [HttpPut("ActualizarRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarRol([FromBody] Rol rol)
        {
            try
            {
                if (rol == null)
                    return BadRequest("Los datos del rol no pueden ser nulos.");

                var rolActualizado = await _rolRepository.ActualizarRol(rol);
                return Ok(rolActualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el rol.");
            }
        }

        // -------------------- DELETE: api/rol/EliminarRol/{idrol} --------------------
        [HttpDelete("EliminarRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarRol(Guid idrol)
        {
            try
            {
                var eliminado = await _rolRepository.EliminarRol(idrol);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar el rol. Verifique que exista.");

                return Ok("Rol eliminado correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el rol.");
            }

        }
    }
}