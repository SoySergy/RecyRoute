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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        // -------------------- GET: api/usuario/ObtenerUsuarios --------------------
        [HttpGet("ObtenerUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepository.ObtenerUsuarios();

                if (usuarios == null || !usuarios.Any())
                    return NotFound("No se encontraron usuarios registrados.");

                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios.");
            }
        }

        // -------------------- GET: api/usuario/ObtenerUsuarioPorId/{idusuario} --------------------
        [HttpGet("ObtenerUsuarioPorId/{idusuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUsuarioPorId(Guid idusuario)
        {
            try
            {
                var usuario = await _usuarioRepository.ObtenerUsuario(idusuario);

                if (usuario == null)
                    return NotFound("No se encontró el usuario solicitado.");

                // Incluye sus datos de Rol y TipoDocumento gracias al Include() del repositorio
                return Ok(usuario);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario.");
            }
        }

        // -------------------- POST: api/usuario/CrearUsuario --------------------
        [HttpPost("CrearUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest("Los datos del usuario no pueden ser nulos.");

                // Validación simple de claves foráneas
                if (usuario.IdRol == Guid.Empty || usuario.IdTipoDocumento == Guid.Empty)
                    return BadRequest("El usuario debe tener un IdRol y un IdTipoDocumento válidos.");

                var nuevoUsuario = await _usuarioRepository.CrearUsuario(usuario);
                return Ok(nuevoUsuario);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario.");
            }
        }

        // -------------------- PUT: api/usuario/ActualizarUsuario --------------------
        [HttpPut("ActualizarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest("Los datos del usuario no pueden ser nulos.");

                if (usuario.IdUsuario == Guid.Empty)
                    return BadRequest("El IdUsuario es obligatorio para actualizar un registro.");

                var actualizado = await _usuarioRepository.ActualizarUsuario(usuario);
                return Ok(actualizado);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario.");
            }
        }

        // -------------------- DELETE: api/usuario/EliminarUsuario/{idusuario} --------------------
        [HttpDelete("EliminarUsuario/{idusuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarUsuario(Guid idusuario)
        {
            try
            {
                var eliminado = await _usuarioRepository.EliminarUsuario(idusuario);

                if (!eliminado)
                    return BadRequest("No se pudo eliminar el usuario. Verifique que exista.");

                return Ok("Usuario eliminado correctamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario.");
            }

        }
    }
}
