using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CRUD1_M.Controllers.Impl
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : Controller
    {
        private UserServiceImpl userServiceImpl;
        private IConfiguration configuration;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            userServiceImpl = new UserServiceImpl(new Repositories.Impl.UserRepository(new Utils.DbUtil(configuration.GetConnectionString("paa"))));
        }

        [HttpPost("register")]
        public ActionResult<User> register([FromBody] RegisterDTO dto)
        {
            User user = this.userServiceImpl.register(dto);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> login([FromBody] LoginDTO dto)
        {
            User user = this.userServiceImpl.login(dto, this.configuration);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
