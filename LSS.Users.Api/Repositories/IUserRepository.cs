using LSS.Users.Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Users.Api.Repositories
{
    public interface IUserRepository : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>, IUserLoginStore<User>
    {
    }
}
