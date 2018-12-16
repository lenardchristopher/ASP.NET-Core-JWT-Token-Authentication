using LSS.Users.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace LSS.Users.Api.Controllers.Models
{
    public class RegisterRequest
    {
        [BindRequired]
        [EmailAddress]
        public string Email { get; set; }

        [BindRequired]
        public string FirstName { get; set; }

        [BindRequired]
        public string LastName { get; set; }

        [BindRequired]
        public string Password { get; set; }

        [BindRequired]
        [Phone]
        public string Phone { get; set; }

        public void CopyToModel(User model)
        {
            model.UserName = Email;
            model.Email = Email;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.PhoneNumber = Phone;
        }
    }
}
