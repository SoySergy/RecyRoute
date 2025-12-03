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
    public class HistorialController : ControllerBase
    {
        private readonly IHistorialRepository _historialRepository;

        public HistorialController(IHistorialRepository historialRepository)
        {
            _historialRepository = historialRepository;
        }

        // -------------------- GET: api/historial/ObtenerTodos --------------------
        [HttpGet("ObtenerTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var historiales = await _historialRepository.ObtenerTodosAsync();

                if (historiales == null || !historiales.Any())
                    return NotFound("No se encontraron registros en el historial.");

                return Ok(historiales);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el historial.");
            }
        }

        // -------------------- GET: api/historial/ObtenerPorId/{idHistorial} --------------------
        [HttpGet("ObtenerPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorId(Guid idHistorial)
        {
            try
            {
                var historial = await _historialRepository.ObtenerPorId(idHistorial);

                if (historial == null)
                    return NotFound("No se encontró el registro del historial solicitado.");

                return Ok(historial);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro del historial.");
            }
        }

        // -------------------- GET: api/historial/ObtenerPorSolicitud/{idSolicitud} --------------------
        [HttpGet("ObtenerPorSolicitud")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorSolicitud(Guid idSolicitud)
        {
            try
            {
                var historiales = await _historialRepository.ObtenerPorSolicitud(idSolicitud);

                if (historiales == null || !historiales.Any())
                    return NotFound("No se encontró historial para esta solicitud.");

                return Ok(historiales);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el historial de la solicitud.");
            }
        }

        // -------------------- GET: api/historial/ObtenerPorUsuario/{idUsuario} --------------------
        [HttpGet("ObtenerPorUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorUsuario(Guid idUsuario)
        {
            try
            {
                var historiales = await _historialRepository.ObtenerPorUsuario(idUsuario);

                if (historiales == null || !historiales.Any())
                    return NotFound("No se encontraron cambios realizados por este usuario.");

                return Ok(historiales);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el historial del usuario.");
            }
        }

        // -------------------- GET: api/historial/ObtenerPorEstadoNuevo/{estadoNuevo} --------------------
        [HttpGet("ObtenerPorEstadoNuevo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorEstadoNuevo(string estadoNuevo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estadoNuevo))
                    return BadRequest("El estado no puede estar vacío.");

                var historiales = await _historialRepository.ObtenerPorEstadoNuevo(estadoNuevo);

                if (historiales == null || !historiales.Any())
                    return NotFound($"No se encontraron registros con el estado '{estadoNuevo}'.");

                return Ok(historiales);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el historial por estado.");
            }
        }

        // -------------------- GET: api/historial/ObtenerPorFecha --------------------
        [HttpGet("ObtenerPorFecha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorFecha([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                if (fechaInicio > fechaFin)
                    return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin.");

                var historiales = await _historialRepository.ObtenerPorFecha(fechaInicio, fechaFin);

                if (historiales == null || !historiales.Any())
                    return NotFound("No se encontraron registros en el rango de fechas especificado.");

                return Ok(historiales);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el historial por fecha.");
            }
        }

        // -------------------- POST: api/historial/CrearHistorial --------------------
        [HttpPost("CrearHistorial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearHistorial([FromBody] Historial historial)
        {
            try
            {
                if (historial == null)
                    return BadRequest("Los datos del historial no pueden ser nulos.");

                if (historial.IdSolicitud == Guid.Empty || historial.IdUsuario == Guid.Empty)
                    return BadRequest("El historial debe tener un IdSolicitud y un IdUsuario válidos.");

                if (string.IsNullOrWhiteSpace(historial.EstadoNuevo))
                    return BadRequest("El estado nuevo es obligatorio.");

                var nuevoHistorial = await _historialRepository.Crear(historial);
                return Ok(nuevoHistorial);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro del historial.");
            }
        }

        // -------------------- PUT: api/historial/ActualizarHistorial --------------------
        [HttpPut("ActualizarHistorial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarHistorial([FromBody] Historial historial)
        {
            try
            {
                if (historial == null)
                    return BadRequest("Los datos del historial no pueden ser nulos.");

                if (historial.IdHistorial == Guid.Empty)
                    return BadRequest("El IdHistorial es obligatorio para actualizar un registro.");

                // Verificar si existe antes de actualizar
                var existe = await _historialRepository.Existe(historial.IdHistorial);
                if (!existe)
                    return NotFound("El registro del historial no existe.");

                var actualizado = await _historialRepository.Actualizar(historial);

                if (actualizado == null)
                    return BadRequest("No se pudo actualizar el registro del historial.");

                return Ok(actualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro del historial.");
            }
        }

        // -------------------- DELETE: api/historial/EliminarHistorial/{idHistorial} --------------------
        [HttpDelete("EliminarHistorial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarHistorial(Guid idHistorial)
        {
            try
            {
                var eliminado = await _historialRepository.Eliminar(idHistorial);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar el registro. Verifique que exista.");

                return Ok("Registro del historial eliminado correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro del historial.");
            }
        }

        // -------------------- GET: api/historial/ExisteHistorial/{idHistorial} --------------------
        [HttpGet("ExisteHistorial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExisteHistorial(Guid idHistorial)
        {
            try
            {
                var existe = await _historialRepository.Existe(idHistorial);
                return Ok(new { existe = existe });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al verificar la existencia del historial.");
            }
        }
    }
}