using System.Collections.Generic;
using IntergrationA.Models;
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

        public UsersController(IUserService userService, ILogger<UsersController> _userlog)
        {
            _userService = userService;
            userlog = _userlog;

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

        [Route("addadminuser")]
        [Authorize]
        [HttpPost]
        public ActionResult adminusers([FromBody] List<userinfoschema> usersinfo)
        {
            var users = _userService.postuser(usersinfo);
            if (users)
            {
                return Ok(users);
            }
            else
            {
                return NoContent();
            }

        }
    }
}
