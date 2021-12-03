using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private ILogger<UsersController> userlog;

        public UsersController(IUserService userService,ILogger<UsersController> _userlog)
        {
            _userService = userService;
            userlog=_userlog;
            
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            userlog.LogInformation("Authenticate is called ");
            userlog.LogError("Authenticate is called ");
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {

            var users = _userService.GetAll();
            return Ok(users);
        }
        [Route("adminusers")]
        [Authorize]
        [HttpGet]
        public IActionResult adminusers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }
    }
}
