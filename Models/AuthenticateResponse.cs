using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }        
        public string[] Token { get; set; }


        public AuthenticateResponse(string user, string[] token)
        {
            Id = user;           
            Token = token;
            
        }
    }
}