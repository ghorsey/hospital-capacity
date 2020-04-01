namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Spa.Models.Authorization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AuthorizationController.
    /// Implements the <see cref="Gah.HC.Spa.Controllers.BaseController" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Spa.Controllers.BaseController" />
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// userManager
        /// or
        /// signInManager.
        /// </exception>
        public AuthorizationController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AuthorizationController> logger)
            : base(logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <summary>
        /// Registers the super user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("register/super")]
        public async Task<IActionResult> RegisterSuperUser(RegisterUser input, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Registering a super user");

            if (input == null)
            {
                return this.BadRequest("null input");
            }

            if (!this.ModelState.IsValid)
            {
                this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var user = new AppUser
            {
                UserName = input.Email,
                UserType = AppUserType.Admin,
            };

            cancellationToken.ThrowIfCancellationRequested();

            var result = await this.userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            await this.signInManager.SignInAsync(user, isPersistent: false);

            return this.NoContent();
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(LoginModel input, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Attempting to log in a user");

            if (input == null)
            {
                return this.BadRequest("input cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            cancellationToken.ThrowIfCancellationRequested();

            var result = await this.signInManager.PasswordSignInAsync(
                input.Email,
                input.Password,
                input.RememberMe,
                false);

            if (result.Succeeded)
            {
                return this.NoContent();
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
        }

        /// <summary>
        /// Gets me.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            this.Logger.LogInformation("Getting own record");

            var result = await this.userManager.GetUserAsync(this.User);

            var dto = new UserDto
            {
                HospitalId = result.HospitalId,
                RegionId = result.RegionId,
                UserType = result.UserType,
            };

            return this.Ok(dto.MakeSuccessfulResult());
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await this.signInManager.SignOutAsync();

            return this.NoContent();
        }
    }
}
