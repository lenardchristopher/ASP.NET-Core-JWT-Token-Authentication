using LSS.Users.Api.Controllers.Models;
using LSS.Users.Api.Models;
using LSS.Users.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LSS.Users.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UsersController> _logger;
        private readonly ITokenManager _tokenManager;

        public UsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<UsersController> logger,
            ITokenManager tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenManager = tokenManager;
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<object>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                // generate a jwt token
                var token = _tokenManager.GenerateToken(user);

                return Ok(new
                {
                    authToken = token,
                    firstName = "",
                    lastName = ""
                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid password.");
                return BadRequest();
            }
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<object>> RegisterAsync(RegisterRequest request)
        {
            var user = new User();
            request.CopyToModel(user);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // generate a jwt token
                var token = _tokenManager.GenerateToken(user);

                return Ok(new
                {
                    authToken = token,
                    firstName = "",
                    lastName = ""
                });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest();
        }
    }
}
