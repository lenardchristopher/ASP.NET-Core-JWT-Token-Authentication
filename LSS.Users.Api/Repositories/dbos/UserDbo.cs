using LSS.Users.Api.Models;
using System;

namespace LSS.Users.Api.Repositories.dbos
{
    public class UserDbo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }

        public void CopyToModel(User model)
        {
            model.Id = Id;
            model.Email = Email;
            model.EmailConfirmed = EmailConfirmed; 
            model.PasswordHash = PasswordHash; 
            model.UserName = UserName;
            model.PhoneNumber = PhoneNumber;
            model.PhoneNumberConfirmed = PhoneNumberConfirmed;
            model.SecurityStamp = SecurityStamp;
            model.TwoFactorEnabled = TwoFactorEnabled;
            model.LockoutEnabled = LockoutEnabled;
            model.AccessFailedCount = AccessFailedCount;
            model.LockoutEnd = LockoutEndDateUtc;
        }
    }
}
