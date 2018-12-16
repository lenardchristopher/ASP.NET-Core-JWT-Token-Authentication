using Microsoft.AspNetCore.Identity;

namespace LSS.Users.Api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
