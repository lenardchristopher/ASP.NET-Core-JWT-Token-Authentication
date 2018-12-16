using LSS.Users.Api.Models;

namespace LSS.Users.Api.Services
{
    public interface ITokenManager
    {
        string GenerateToken(User user);
    }
}
