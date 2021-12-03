using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]

    public class Authenticate : ControllerBase
    {
        private IUserService _userService;
        public Authenticate(IUserService userService)
        {
            _userService = userService;
        }
    }

}