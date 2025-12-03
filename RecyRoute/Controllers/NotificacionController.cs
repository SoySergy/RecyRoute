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
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionController(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        // -------------------- GET: api/notificacion/ObtenerNotificaciones --------------------
        [HttpGet("ObtenerNotificaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerNotificaciones()
        {
            try
            {
                var notificaciones = await _notificacionRepository.ObtenerNotificaciones();

                if (notificaciones == null || !notificaciones.Any())
                    return NotFound("No se encontraron notificaciones registradas.");

                return Ok(notificaciones);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las notificaciones.");
            }
        }

        // -------------------- GET: api/notificacion/ObtenerNotificacionPorId/{idnotificacion} --------------------
        [HttpGet("ObtenerNotificacionPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerNotificacionPorId(Guid idnotificacion)
        {
            try
            {
                var notificacion = await _notificacionRepository.ObtenerNotificacion(idnotificacion);

                if (notificacion == null)
                    return NotFound("No se encontró la notificación solicitada.");

                return Ok(notificacion);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la notificación.");
            }
        }

        // -------------------- POST: api/notificacion/CrearNotificacion --------------------
        [HttpPost("CrearNotificacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearNotificacion([FromBody] Notificacion notificacion)
        {
            try
            {
                if (notificacion == null)
                    return BadRequest("Los datos de la notificación no pueden ser nulos.");

                if (string.IsNullOrWhiteSpace(notificacion.Titulo) || string.IsNullOrWhiteSpace(notificacion.Mensaje))
                    return BadRequest("El título y el mensaje son obligatorios.");

                var nuevaNotificacion = await _notificacionRepository.CrearNotificacion(notificacion);
                return Ok(nuevaNotificacion);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la notificación.");
            }
        }

        // -------------------- PUT: api/notificacion/ActualizarNotificacion --------------------
        [HttpPut("ActualizarNotificacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarNotificacion([FromBody] Notificacion notificacion)
        {
            try
            {
                if (notificacion == null)
                    return BadRequest("Los datos de la notificación no pueden ser nulos.");

                if (notificacion.IdNotificacion == Guid.Empty)
                    return BadRequest("El IdNotificacion es obligatorio para actualizar un registro.");

                var actualizada = await _notificacionRepository.ActualizarNotificacion(notificacion);
                return Ok(actualizada);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la notificación.");
            }
        }

        // -------------------- PATCH: api/notificacion/MarcarComoLeida/{idnotificacion} --------------------
        [HttpPatch("MarcarComoLeida")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarcarComoLeida(Guid idnotificacion)
        {
            try
            {
                var notificacion = await _notificacionRepository.ObtenerNotificacion(idnotificacion);

                if (notificacion == null)
                    return NotFound("No se encontró la notificación solicitada.");

                notificacion.Leida = true;
                var actualizada = await _notificacionRepository.ActualizarNotificacion(notificacion);
                return Ok(actualizada);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al marcar la notificación como leída.");
            }
        }

        // -------------------- DELETE: api/notificacion/EliminarNotificacion/{idnotificacion} --------------------
        [HttpDelete("EliminarNotificacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarNotificacion(Guid idnotificacion)
        {
            try
            {
                var eliminada = await _notificacionRepository.EliminarNotificacion(idnotificacion);

                if (!eliminada)
                    return BadRequest("No se pudo eliminar la notificación. Verifique que exista.");

                return Ok("Notificación eliminada correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la notificación.");
            }
        }
    }
}
