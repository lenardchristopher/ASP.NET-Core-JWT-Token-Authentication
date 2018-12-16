using Dapper;
using Lss.Framework.Repositories;
using LSS.Users.Api.Models;
using LSS.Users.Api.Repositories.dbos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace LSS.Users.Api.Repositories
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(IOptions<ApplicationSettings> options) : base(options.Value.SqlServerConnectionString) { }

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO Users (Id,Email,EmailConfirmed,PasswordHash,UserName,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,LockoutEndDateUtc) " +
                " VALUES(@Id,@Email,@EmailConfirmed,@Password,@UserName,@PhoneNumber,@PhoneNumberConfirmed,@SecurityStamp,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount,@LockoutEndDateUtc)";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@EmailConfirmed", user.EmailConfirmed, DbType.Boolean);
            parameters.Add("@Password", user.PasswordHash, DbType.String);
            parameters.Add("@UserName", user.UserName, DbType.String);
            parameters.Add("@PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("@PhoneNumberConfirmed", user.PhoneNumberConfirmed, DbType.Boolean);
            parameters.Add("@SecurityStamp", user.PhoneNumber, DbType.String);
            parameters.Add("@TwoFactorEnabled", user.TwoFactorEnabled, DbType.Boolean);
            parameters.Add("@LockoutEnabled", user.LockoutEnabled, DbType.Boolean);
            parameters.Add("@AccessFailedCount", user.AccessFailedCount, DbType.Int32);
            parameters.Add("@LockoutEndDateUtc", user.LockoutEnd, DbType.DateTime); 

            try
            {
                using (var conn = GetOpenConnection())
                {
                    await conn.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle this better. User probably already exists or something else went wrong.
                throw ex;
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Email, DbType.String);

            try
            {
                using (var conn = GetOpenConnection())
                {
                    await conn.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle this better.
                throw ex;
            }
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Email FROM Users WHERE Email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", normalizedEmail, DbType.String);

            UserDbo dbo = null;
            using (var conn = GetOpenConnection())
            {
                dbo = await conn.QueryFirstAsync<UserDbo>(sql, parameters);
            }

            User model = new User();
            dbo.CopyToModel(model);
            return model;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Email FROM Users WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String);

            UserDbo dbo = null;
            using (var conn = GetOpenConnection())
            {
                dbo = await conn.QueryFirstAsync<UserDbo>(sql, parameters);
            }

            User model = new User();
            dbo.CopyToModel(model);
            return model;
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id,Email,EmailConfirmed,PasswordHash,UserName,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,LockoutEndDateUtc" + 
                " FROM Users WHERE Email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", normalizedUserName, DbType.String);

            UserDbo dbo = null;
            using (var conn = GetOpenConnection())
            {
                dbo = await conn.QueryFirstOrDefaultAsync<UserDbo>(sql, parameters);
            }

            User model = null;
            if (dbo != null)
            {
                model = new User();
                dbo.CopyToModel(model);
            }
            return model;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.EmailConfirmed);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.UserName);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Users SET "
                + "Email = @Email";
            sql += " WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);

            using (var conn = GetOpenConnection())
            {
                await conn.ExecuteAsync(sql, parameters);
            }

            return IdentityResult.Success;
        }
    }
}
