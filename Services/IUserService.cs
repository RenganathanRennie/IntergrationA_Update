using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;
using static WebApi.Models.testmodel;

public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        IEnumerable<SLBAdminUser> GetUsers();

        User GetById(int id);
    }
