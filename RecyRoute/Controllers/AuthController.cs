using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RecyRoute.Repositories.Interfaces;

namespace RecyRoute.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]

    public class AuthController: ControllerBase
    {
        private readonly IUsuarioRepository _usarUsuarioRepository;
        private readonly IConfiguration _configuration;

        //public AuthController(IUsuarioRepository usuarioRepository)

        public AuthController(IUsuarioRepository userRepository)
        {
            _usarUsuarioRepository = userRepository;
        }


    }



}
