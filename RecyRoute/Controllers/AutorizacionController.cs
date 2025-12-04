using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using RecyRoute.Modelos;
using RecyRoute.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecyRoute.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController : ControllerBase
    {
      private readonly IUsuarioRepository _usuarioRepository;
      private readonly IConfiguration _configuration;

        public AutorizacionController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }
        [AllowAnonymous]  // No pide token
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contrasena))
            {
                return BadRequest("Invalid client request");
            }

            var Usuario = await _usuarioRepository.ObtenerUsuarioPorCorreo(login.Correo);
            if (Usuario == null)
            {
                return Unauthorized();
            }

            // Verificar las credenciales
            if (login.Contrasena == Usuario.Contrasena)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, login.Correo), // Usamos el email como nombre de usuario
                    new Claim(ClaimTypes.Role,Usuario.Rol.NombreRol) // Puedes ajustar el rol según sea necesario
                    },
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
