using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;
public interface IAuth
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model); 
    }
