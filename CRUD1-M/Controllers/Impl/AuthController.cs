using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Services.Impl;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRUD1_M.Controllers.Impl
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private string credentials;
        private IConfiguration configuration;
        private AuthServiceImpl authService;
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.credentials = configuration.GetConnectionString("paa");
            this.authService = new AuthServiceImpl(new Repositories.Impl.AuthRepositoryImpl(new Utils.DbUtil(this.credentials)));
        }

        [HttpPost("register")]
        public ActionResult<User> register([FromBody] RegisterDTO dto)
        {
            User user = new User();
            user.name = dto.name;
            user.email = dto.email;
            user.password = dto.password;
            if(this.authService.register(dto) == null)
            {
                return BadRequest();
            }
            return Ok(this.authService.register(dto));
        }

        [HttpPost("login")]
        public ActionResult<User> login([FromBody] LoginDTO dto)
        {
            User user = new User();
            user.email = dto.email;
            user.password = dto.password;
            if(this.authService.login(dto, configuration) == null)
            {
                return BadRequest();
            }
            return Ok(this.authService.login(dto, configuration));
        }
    }
}
