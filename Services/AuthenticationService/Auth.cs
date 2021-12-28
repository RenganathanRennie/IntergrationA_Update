
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using static WebApi.Models.testmodel;

namespace WebApi.Services
{
    // public interface IUserService
    // {
    //     AuthenticateResponse Authenticate(AuthenticateRequest model);
    //     IEnumerable<User> GetAll();
    //     User GetById(int id);
    // }

    public class Auth : IAuth
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        // private List<User> _users = new List<User>
        // {
        //     new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        // };

        private readonly AppSettings _appSettings;
        private readonly DataBaseContext _dbcon;
        private ILogger<Auth> _authlog;
        public Auth(IOptions<AppSettings> appSettings, DataBaseContext con, ILogger<Auth> authlog)
        {
            _appSettings = appSettings.Value;
            _dbcon = con;
            _authlog = authlog;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        { 

             
            var user = await Task.Run(()=> _dbcon.userinfoschema.Where(k=>k.name==model.Username && k.password==model.Password).FirstOrDefault());
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user.name);

            return new AuthenticateResponse(user.name, token);
             
        }



        // helper methods

        private async Task<string[]> generateJwtToken(string userid)
        {
            // generate token that is valid for 7 days

                 // get token expiry 

                var expirytime =await Task.Run(()=> Convert.ToInt32(_dbcon.Settings.Where(x => x.settingsId == "set001").Select(v => v.settingsValue).FirstOrDefault()));
                _authlog.LogInformation(" expiry time " + expirytime.ToString());
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id",userid) }),
                    Expires = DateTime.UtcNow.AddHours(expirytime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new string[]{tokenHandler.WriteToken(token),token.ValidFrom.ToLocalTime().ToString(),token.ValidTo.ToLocalTime().ToString()};
           
        }
    }
}