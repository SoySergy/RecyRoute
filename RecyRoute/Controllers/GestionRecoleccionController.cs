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
    public class GestionRecoleccionController : ControllerBase
    {
        private readonly IGestionRecoleccionRepository _gestionRecoleccionRepository;

        public GestionRecoleccionController(IGestionRecoleccionRepository gestionRecoleccionRepository)
        {
            _gestionRecoleccionRepository = gestionRecoleccionRepository;
        }

        // -------------------- GET: api/gestionrecoleccion/ObtenerGestionesRecoleccion --------------------
        [HttpGet("ObtenerGestionesRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGestionesRecoleccion()
        {
            try
            {
                var gestiones = await _gestionRecoleccionRepository.ObtenerGestionesRecoleccion();

                if (gestiones == null || !gestiones.Any())
                    return NotFound("No se encontraron gestiones de recolección registradas.");

                return Ok(gestiones);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las gestiones de recolección.");
            }
        }

        // -------------------- GET: api/gestionrecoleccion/ObtenerGestionRecoleccionPorId/{idGestion} --------------------
        [HttpGet("ObtenerGestionRecoleccionPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerGestionRecoleccionPorId(Guid idGestion)
        {
            try
            {
                var gestion = await _gestionRecoleccionRepository.ObtenerGestionRecoleccion(idGestion);

                if (gestion == null)
                    return NotFound("No se encontró la gestión de recolección solicitada.");

                // Incluye los datos de SolicitudRecoleccion y Gestor gracias al Include() del repositorio
                return Ok(gestion);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la gestión de recolección.");
            }
        }

        // -------------------- POST: api/gestionrecoleccion/CrearGestionRecoleccion --------------------
        [HttpPost("CrearGestionRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearGestionRecoleccion([FromBody] GestionRecoleccion gestionRecoleccion)
        {
            try
            {
                if (gestionRecoleccion == null)
                    return BadRequest("Los datos de la gestión de recolección no pueden ser nulos.");

                // Validación simple de claves foráneas
                if (gestionRecoleccion.IdSolicitud == Guid.Empty)
                    return BadRequest("La gestión debe tener un IdSolicitud válido.");

                if (gestionRecoleccion.IdGestor == Guid.Empty)
                    return BadRequest("La gestión debe tener un IdGestor válido.");

                var nuevaGestion = await _gestionRecoleccionRepository.CrearGestionRecoleccion(gestionRecoleccion);
                return Ok(nuevaGestion);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la gestión de recolección.");
            }
        }

        // -------------------- PUT: api/gestionrecoleccion/ActualizarGestionRecoleccion --------------------
        [HttpPut("ActualizarGestionRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarGestionRecoleccion([FromBody] GestionRecoleccion gestionRecoleccion)
        {
            try
            {
                if (gestionRecoleccion == null)
                    return BadRequest("Los datos de la gestión de recolección no pueden ser nulos.");

                if (gestionRecoleccion.IdGestion == Guid.Empty)
                    return BadRequest("El IdGestion es obligatorio para actualizar un registro.");

                var actualizado = await _gestionRecoleccionRepository.ActualizarGestionRecoleccion(gestionRecoleccion);
                return Ok(actualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la gestión de recolección.");
            }
        }

        // -------------------- DELETE: api/gestionrecoleccion/EliminarGestionRecoleccion/{idGestion} --------------------
        [HttpDelete("EliminarGestionRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarGestionRecoleccion(Guid idGestion)
        {
            try
            {
                var eliminado = await _gestionRecoleccionRepository.EliminarGestionRecoleccion(idGestion);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar la gestión de recolección. Verifique que exista.");

                return Ok("Gestión de recolección eliminada correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la gestión de recolección.");
            }
        }
    }
}
