using System.Collections.Generic;
using System.Threading.Tasks;
using IntergrationA.Models;
using WebApi.Entities;
using WebApi.Models;
using static WebApi.Models.testmodel;

public interface IUserService
    {
      //  AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        IEnumerable<SLBAdminUser> GetUsers();

        Task<User> GetbyName(string id);
//test user model 
      bool postuser(List<userinfoschema> userinfoschemas);

    }
