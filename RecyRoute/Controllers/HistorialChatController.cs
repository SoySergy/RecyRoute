using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialChatController : ControllerBase
    {
        private readonly IHistorialChatRepository _historialChatRepository;

        public HistorialChatController(IHistorialChatRepository historialChatRepository)
        {
            _historialChatRepository = historialChatRepository;
        }

        // -------------------- GET: api/historialchat/ObtenerMensajesPorSolicitud/{idSolicitud} --------------------
        [HttpGet("ObtenerMensajesPorSolicitud/{idSolicitud}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerMensajesPorSolicitud(Guid idSolicitud)
        {
            try
            {
                var mensajes = await _historialChatRepository.ObtenerMensajesPorSolicitud(idSolicitud);

                if (mensajes == null || !mensajes.Any())
                    return NotFound("No se encontraron mensajes para esta solicitud.");

                return Ok(mensajes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los mensajes.");
            }
        }

        // -------------------- GET: api/historialchat/ObtenerMensaje/{idHistorialChat} --------------------
        [HttpGet("ObtenerMensaje/{idHistorialChat}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerMensaje(Guid idHistorialChat)
        {
            try
            {
                var mensaje = await _historialChatRepository.ObtenerMensaje(idHistorialChat);

                if (mensaje == null)
                    return NotFound("No se encontró el mensaje solicitado.");

                return Ok(mensaje);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el mensaje.");
            }
        }

        // -------------------- POST: api/historialchat/EnviarMensaje --------------------
        [HttpPost("EnviarMensaje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnviarMensaje([FromBody] HistorialChat historialChat)
        {
            try
            {
                if (historialChat == null)
                    return BadRequest("Los datos del mensaje no pueden ser nulos.");

                if (historialChat.IdSolicitud == Guid.Empty || historialChat.IdEmisor == Guid.Empty)
                    return BadRequest("El mensaje debe tener un IdSolicitud y un IdEmisor válidos.");

                if (string.IsNullOrWhiteSpace(historialChat.Mensaje))
                    return BadRequest("El mensaje no puede estar vacío.");

                var nuevoMensaje = await _historialChatRepository.CrearMensaje(historialChat);
                return Ok(nuevoMensaje);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al enviar el mensaje.");
            }
        }

        // -------------------- PUT: api/historialchat/MarcarComoLeido/{idHistorialChat} --------------------
        [HttpPut("MarcarComoLeido/{idHistorialChat}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarcarComoLeido(Guid idHistorialChat)
        {
            try
            {
                var resultado = await _historialChatRepository.MarcarComoLeido(idHistorialChat);

                if (!resultado)
                    return BadRequest("No se pudo marcar el mensaje como leído. Verifique que exista.");

                return Ok("Mensaje marcado como leído correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al marcar el mensaje como leído.");
            }
        }

        // -------------------- GET: api/historialchat/ObtenerMensajesNoLeidos/{idUsuario}/{idSolicitud} --------------------
        [HttpGet("ObtenerMensajesNoLeidos/{idUsuario}/{idSolicitud}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerMensajesNoLeidos(Guid idUsuario, Guid idSolicitud)
        {
            try
            {
                var mensajes = await _historialChatRepository.ObtenerMensajesNoLeidos(idUsuario, idSolicitud);

                if (mensajes == null || !mensajes.Any())
                    return Ok(new List<HistorialChat>());

                return Ok(mensajes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los mensajes no leídos.");
            }
        }

        // -------------------- DELETE: api/historialchat/EliminarMensaje/{idHistorialChat} --------------------
        [HttpDelete("EliminarMensaje/{idHistorialChat}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarMensaje(Guid idHistorialChat)
        {
            try
            {
                var eliminado = await _historialChatRepository.EliminarMensaje(idHistorialChat);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar el mensaje. Verifique que exista.");

                return Ok("Mensaje eliminado correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el mensaje.");
            }
        }
    }
}