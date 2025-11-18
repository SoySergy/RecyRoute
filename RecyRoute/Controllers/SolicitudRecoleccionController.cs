using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecyRoute.Modelos;
using RecyRoute.Repositories;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudRecoleccionController : ControllerBase
    {
        private readonly ISolicitudRecoleccionRepository _solicitudRecoleccionRepository;

        public SolicitudRecoleccionController(ISolicitudRecoleccionRepository solicitudRecoleccionRepository)
        {
            _solicitudRecoleccionRepository = solicitudRecoleccionRepository;
        }

        // -------------------- GET: api/solicitudrecoleccion/ObtenerSolicitudesRecoleccion --------------------
        [HttpGet("ObtenerSolicitudesRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerSolicitudesRecoleccion()
        {
            try
            {
                var solicitudes = await _solicitudRecoleccionRepository.ObtenerSolicitudesRecoleccion();

                if (solicitudes == null || !solicitudes.Any())
                    return NotFound("No se encontraron solicitudes de recolección registradas.");

                return Ok(solicitudes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las solicitudes de recolección.");
            }
        }

        // -------------------- GET: api/solicitudrecoleccion/ObtenerSolicitudRecoleccionPorId/{idSolicitud} --------------------
        [HttpGet("ObtenerSolicitudRecoleccionPorId/{idSolicitud}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerSolicitudRecoleccionPorId(Guid idSolicitud)
        {
            try
            {
                var solicitud = await _solicitudRecoleccionRepository.ObtenerSolicitudRecoleccion(idSolicitud);

                if (solicitud == null)
                    return NotFound("No se encontró la solicitud de recolección solicitada.");

                // Incluye los datos del Usuario gracias al Include() del repositorio
                return Ok(solicitud);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la solicitud de recolección.");
            }
        }

        // -------------------- POST: api/solicitudrecoleccion/CrearSolicitudRecoleccion --------------------
        [HttpPost("CrearSolicitudRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearSolicitudRecoleccion([FromBody] SolicitudRecoleccion solicitudRecoleccion)
        {
            try
            {
                if (solicitudRecoleccion == null)
                    return BadRequest("Los datos de la solicitud de recolección no pueden ser nulos.");

                // Validación simple de clave foránea
                if (solicitudRecoleccion.IdUsuario == Guid.Empty)
                    return BadRequest("La solicitud debe tener un IdUsuario válido.");

                var nuevaSolicitud = await _solicitudRecoleccionRepository.CrearSolicitudRecoleccion(solicitudRecoleccion);
                return Ok(nuevaSolicitud);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la solicitud de recolección.");
            }
        }

        // -------------------- PUT: api/solicitudrecoleccion/ActualizarSolicitudRecoleccion --------------------
        [HttpPut("ActualizarSolicitudRecoleccion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarSolicitudRecoleccion([FromBody] SolicitudRecoleccion solicitudRecoleccion)
        {
            try
            {
                if (solicitudRecoleccion == null)
                    return BadRequest("Los datos de la solicitud de recolección no pueden ser nulos.");

                if (solicitudRecoleccion.IdSolicitud == Guid.Empty)
                    return BadRequest("El IdSolicitud es obligatorio para actualizar un registro.");

                var actualizado = await _solicitudRecoleccionRepository.ActualizarSolicitudRecoleccion(solicitudRecoleccion);
                return Ok(actualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la solicitud de recolección.");
            }
        }

        // -------------------- DELETE: api/solicitudrecoleccion/EliminarSolicitudRecoleccion/{idSolicitud} --------------------
        [HttpDelete("EliminarSolicitudRecoleccion/{idSolicitud}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarSolicitudRecoleccion(Guid idSolicitud)
        {
            try
            {
                var eliminado = await _solicitudRecoleccionRepository.EliminarSolicitudRecoleccion(idSolicitud);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar la solicitud de recolección. Verifique que exista.");

                return Ok("Solicitud de recolección eliminada correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la solicitud de recolección.");
            }
        }
    }
}

