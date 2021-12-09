using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
     [Route("[controller]")]
    [ApiController]

    public class Authenticate : ControllerBase
    {
        private IAuth iautheticate;
        private ILogger<Authenticate> _authlog;

        public Authenticate(IAuth _iautheticate, ILogger<Authenticate> authlog)
        {
            iautheticate = _iautheticate;
            _authlog = authlog;

        } 
       [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            _authlog.LogInformation("Authenticate is called ");
            var response = await iautheticate.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            _authlog.LogInformation(JsonConvert.SerializeObject(response));
            return Ok(response);
        }
    }

}