using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LSS.Users.Api.Controllers.Models
{
    public class LoginRequest
    {
        [BindRequired]
        public string Username { get; set; }

        [BindRequired]
        public string Password { get; set; }
    }
}
