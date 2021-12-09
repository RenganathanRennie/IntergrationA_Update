
using IntergrationA.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        private readonly DataBaseContext _dbcon;
        private ILogger<UserService> _userservicelog;
        public UserService(IOptions<AppSettings> appSettings, DataBaseContext con, ILogger<UserService> _userservicelog)
        {
            _appSettings = appSettings.Value;
            this._userservicelog = _userservicelog;
            _dbcon = con;
        }

        // public AuthenticateResponse Authenticate(AuthenticateRequest model)
        // {
        //     var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        //     // return null if user not found
        //     if (user == null) return null;

        //     // authentication successful so generate jwt token
        //     var token = generateJwtToken(user);

        //     return new AuthenticateResponse(user, token);
        // }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public IEnumerable<SLBAdminUser> GetUsers()
        {
            var users = _dbcon.SLBAdminUser.ToList();

            return users;

        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public bool postuser(List<userinfoschema> userinfoschemas)
        {
            bool result = false;
            try
            {
                using (DataBaseContext db = _dbcon)
                {
                    //using block automaticall rollback the transaction no need to call rollback commit at the end of the statement #Renga

                    db.userinfoschema.AddRangeAsync(userinfoschemas);
                    db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _userservicelog.LogError(ex.Message);
                _userservicelog.LogError(ex.Source);
            }
            return result;

        }


        // helper methods

        // private string generateJwtToken(User user)
        // {
        //     // generate token that is valid for 7 days
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //         Expires = DateTime.UtcNow.AddHours(10),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
    }
}