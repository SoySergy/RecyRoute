using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace RecyRoute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController: ControllerBase
    {
        private readonly IUserRepostory _userRepository;
        private readonly IConfiguration _configuration;


    }



}
