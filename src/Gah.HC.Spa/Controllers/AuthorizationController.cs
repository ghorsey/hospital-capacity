namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands;
    using Gah.HC.Commands.Exceptions;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Authorization;
    using Gah.HC.Spa.Models.Shared;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AuthorizationController.
    /// Implements the <see cref="Gah.HC.Spa.Controllers.BaseController" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Spa.Controllers.BaseController" />
    [ApiController]
    [Authorize]
    [Route("api/authorization")]
    public class AuthorizationController : BaseController
    {
        private readonly IDomainBus domainBus;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IAuthorizationService authorizationService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController" /> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="authorizationService">The authorization service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userManager
        /// or
        /// signInManager.</exception>
        public AuthorizationController(
            SignInManager<AppUser> signInManager,
            IDomainBus domainBus,
            IAuthorizationService authorizationService,
            IMapper mapper,
            ILogger<AuthorizationController> logger)
            : base(logger)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Registers the super user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("register/super")]
        [Authorize(Roles="Admin")]
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterSuperUserAsync(RegisterSuperUserInput input, CancellationToken cancellationToken)
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

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var corrId = this.HttpContext.TraceIdentifier;
                await this.domainBus.ExecuteAsync(new RegisterSuperUserCommand(input.Email, input.Password, corrId), cancellationToken);
                var user = await this.domainBus.ExecuteAsync(new FindUserByEmailQuery(input.Email, corrId), cancellationToken);
                await this.signInManager.SignInAsync(user, isPersistent: false);

                var dto = this.mapper.Map<UserDto>(user);
                return this.Ok(dto.MakeSuccessfulResult());
            }
            catch (UserCreationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }
        }

        /// <summary>
        /// register hospital user as an asynchronous operation.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("register/hospital")]
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterHospitalUserAsync(RegisterHospitalUserInput input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return this.BadRequest("input cannot be null".MakeUnsuccessfulResult());
            }

            this.Logger.LogInformation($"Registering hospital user for {input.HospitalId}");

            if (input.HospitalId == Guid.Empty)
            {
                this.ModelState.AddModelError("HosptialId", "Guid cannot be an empty guid");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var authResult = await this.authorizationService.AuthorizeAsync(this.User, input, new RegisterHospitalUserRequirement());

            if (!authResult.Succeeded)
            {
                return this.Forbid();
            }

            var hospital = await this.domainBus.ExecuteAsync(new FindHospitalBySlugOrIdQuery(this.HttpContext.TraceIdentifier, input.HospitalId));
            var cmd = new RegisterHospitalUserCommand(input.Email, input.Password, input.HospitalId, hospital.RegionId);

            await this.domainBus.ExecuteAsync(cmd, cancellationToken);
            var user = await this.domainBus.ExecuteAsync(new FindUserByEmailQuery(input.Email, this.HttpContext.TraceIdentifier));

            var dto = this.mapper.Map<UserDto>(user);
            return this.Ok(dto.MakeSuccessfulResult());
        }

        /// <summary>
        /// Registers the region user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("register/region")]
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterRegionUserAsync(RegisterRegionUserInput input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return this.BadRequest("input cannot be null".MakeUnsuccessfulResult());
            }

            this.Logger.LogInformation($"Registering a new Region User for {input.RegionName}");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var command = new RegisterRegionUserCommand(input.Email, input.Password, input.RegionName, this.HttpContext.TraceIdentifier);

            try
            {
                await this.domainBus.ExecuteAsync(command, cancellationToken);
                var user = await this.domainBus.ExecuteAsync(new FindUserByEmailQuery(input.Email, this.HttpContext.TraceIdentifier));

                if (user.IsApproved)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                }

                var dto = this.mapper.Map<UserDto>(user);
                return this.Ok(dto.MakeSuccessfulResult());
            }
            catch (UserCreationException x)
            {
                foreach (var e in x.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, e.Description);
                }

                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult("Unable to create user"));
            }
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LoginUserAsync(LoginModel input, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Attempting to log in a user");

            if (input == null)
            {
                return this.BadRequest("input cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult("Invalid input"));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var correlationId = this.HttpContext.TraceIdentifier;

            var user = await this.domainBus.ExecuteAsync(new FindUserByEmailQuery(input.Email, correlationId), cancellationToken);

            var result = await this.signInManager.PasswordSignInAsync(
                input.Email,
                input.Password,
                input.RememberMe,
                false);

            if (result.Succeeded && user != null && !user.IsApproved)
            {
                return this.BadRequest("An unapproved user cannot log in.".MakeSuccessfulResult());
            }

            if (result.Succeeded)
            {
                return this.NoContent();
            }

            return this.BadRequest(this.ModelState.MakeUnsuccessfulResult("Email/Password is invalid"));
        }

        /// <summary>
        /// change my password as an asynchronous operation.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("me/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeMyPasswordAsync(ChangeMyPasswordInputModel input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return this.BadRequest("input cannot be null".MakeUnsuccessfulResult());
            }

            this.Logger.LogInformation("A logged in user is updating their password");

            if (this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var correlationId = this.HttpContext.TraceIdentifier;

            var user = await this.domainBus.ExecuteAsync(new FindUserByClaimsPrincipalQuery(this.User, correlationId));

            var command = new ChangeUserPasswordCommand(user, input.CurrentPassword, input.NewPassword, correlationId);

            try
            {
                await this.domainBus.ExecuteAsync(command, cancellationToken);
                return this.NoContent();
            }
            catch (ChangePasswordException x)
            {
                foreach (var e in x.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, e.Description);
                }

                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }
        }

        /// <summary>
        /// Gets me.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMeAsync(CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Getting own record");

            var result = await this.domainBus.ExecuteAsync(
                new FindUserByClaimsPrincipalQuery(
                    this.User,
                    this.HttpContext.TraceIdentifier),
                cancellationToken);

            var dto = this.mapper.Map<UserDto>(result);

            return this.Ok(dto.MakeSuccessfulResult());
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LogOutAsync()
        {
            await this.signInManager.SignOutAsync();

            return this.NoContent();
        }
    }
}
