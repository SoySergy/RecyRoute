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
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoController(ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }
        // -------------------- GET: api/tipodocumento/ObtenerTiposDeDocumento --------------------
        [HttpGet("ObtenerTiposDeDocumento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerTiposDeDocumento()
        {
            try
            {
                var tipos = await _tipoDocumentoRepository.ObtenerTiposDeDocumento();

                if (tipos == null || !tipos.Any())
                    return NotFound("No se encontraron tipos de documento registrados.");

                return Ok(tipos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los tipos de documento.");
            }
        }

        // -------------------- GET: api/tipodocumento/ObtenerTipoDocumentoPorId/{id} --------------------
        [HttpGet("ObtenerTipoDocumentoPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerTipoDocumentoPorId(Guid id)
        {
            try
            {
                var tipo = await _tipoDocumentoRepository.ObtenerTipoDocumento(id);

                if (tipo == null)
                    return NotFound("No se encontró el tipo de documento solicitado.");

                return Ok(tipo);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el tipo de documento.");
            }
        }

        // -------------------- POST: api/tipodocumento/CrearTipoDocumento --------------------
        [HttpPost("CrearTipoDocumento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearTipoDocumento([FromBody] TipoDocumento tipoDocumento)
        {
            try
            {
                if (tipoDocumento == null)
                    return BadRequest("Los datos del tipo de documento no pueden ser nulos.");

                var nuevoTipo = await _tipoDocumentoRepository.CrearTipoDocumento(tipoDocumento);
                return Ok(nuevoTipo);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el tipo de documento.");
            }
        }

        // -------------------- PUT: api/tipodocumento/ActualizarTipoDocumento --------------------
        [HttpPut("ActualizarTipoDocumento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarTipoDocumento([FromBody] TipoDocumento tipoDocumento)
        {
            try
            {
                if (tipoDocumento == null)
                    return BadRequest("Los datos del tipo de documento no pueden ser nulos.");

                var actualizado = await _tipoDocumentoRepository.ActualizarTipoDocumento(tipoDocumento);
                return Ok(actualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el tipo de documento.");
            }
        }

        // -------------------- DELETE: api/tipodocumento/EliminarTipoDocumento/{id} --------------------
        [HttpDelete("EliminarTipoDocumento/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarTipoDocumento(Guid id)
        {
            try
            {
                var eliminado = await _tipoDocumentoRepository.EliminarTipoDocumento(id);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar el tipo de documento. Verifique que exista.");

                return Ok("Tipo de documento eliminado correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el tipo de documento.");
            }
        }
    }
}