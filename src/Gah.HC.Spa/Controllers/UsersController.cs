namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using Gah.HC.Spa.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class UsersController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IDomainBus domainBus;
        private readonly IAuthorizationService authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="authorizationService">The authorization service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">domainBus.</exception>
        public UsersController(
            IDomainBus domainBus,
            IAuthorizationService authorizationService,
            IMapper mapper,
            ILogger<UsersController> logger)
            : base(logger)
        {
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(domainBus));
        }

        /// <summary>
        /// find all users as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [Authorize(Policy = "AdminOnlyAccess")]
        public async Task<IActionResult> FindAllUsersAsync(CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Attempting to find all users in the system");

            var q = new FindAppUsersByRegionOrHospitalQuery(this.HttpContext.TraceIdentifier);
            var result = await this.domainBus.ExecuteAsync(q, cancellationToken);

            return this.Ok(
                this.mapper.Map<List<UserDto>>(result)
                .MakeSuccessfulResult());
        }

        /// <summary>
        /// get user as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(string id, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Attempting to get user {id}");

            var user = await this.domainBus.ExecuteAsync(new FindUserByIdQuery(id, this.HttpContext.TraceIdentifier), cancellationToken);

            var authResult = await this.authorizationService.AuthorizeAsync(this.User, user, new ManageUserRequirement());

            if (!authResult.Succeeded)
            {
                return this.Forbid();
            }

            return this.Ok(this.mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// set password as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("{id}/set-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetPasswordAsync(string id, SetPasswordInput input, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));

            this.Logger.LogInformation($"Setting a password on the user's  ({id}) behalf.");

            if (input == null)
            {
                return this.BadRequest("input cannot be null".MakeUnsuccessfulResult());
            }

            // todo: add authorization check
            var correlationId = this.HttpContext.TraceIdentifier;
            var user = await this.domainBus.ExecuteAsync(new FindUserByIdQuery(id, correlationId), cancellationToken);

            try
            {
                await this.domainBus.ExecuteAsync(new SetUserPasswordCommand(user, input.NewPassword, correlationId), cancellationToken);
                return this.NoContent();
            }
            catch (SetPasswordException x)
            {
                foreach (var e in x.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, e.Description);
                }

                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }
        }

        /// <summary>
        /// Sets the authorized.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("{id}/set-authorized")]
        [ProducesResponseType(typeof(Result<AppUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetAuthorized(string id, SetApprovedInput input, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException(nameof(id));

            this.Logger.LogInformation($"Setting authorized to {input} for user {id}");

            if (input == null)
            {
                return this.BadRequest("input cannot be null".MakeUnsuccessfulResult());
            }

            var correlationId = this.HttpContext.TraceIdentifier;
            var user = await this.domainBus.ExecuteAsync(new FindUserByIdQuery(id, correlationId), cancellationToken);

            await this.domainBus.ExecuteAsync(new SetUserIsApprovedCommand(user, input.IsApproved, correlationId));
            user.IsApproved = input.IsApproved;

            return this.Ok(this.mapper.Map<UserDto>(user).MakeSuccessfulResult());
        }
    }
}