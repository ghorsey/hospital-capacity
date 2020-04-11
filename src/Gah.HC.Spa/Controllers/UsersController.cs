namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Authorization;
    using Gah.HC.Spa.Models.Shared;
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
        private readonly IDomainBus domainBus;
        private readonly IAuthorizationService authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="authorizationService">The authorization service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">domainBus.</exception>
        public UsersController(IDomainBus domainBus, IAuthorizationService authorizationService, ILogger<UsersController> logger)
            : base(logger)
        {
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
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
                result.Select(
                    r => new UserDto
                    {
                        HospitalId = r.HospitalId,
                        Id = r.Id,
                        RegionId = r.RegionId,
                        UserName = r.UserName,
                        UserType = r.UserType,
                    }).ToList().MakeSuccessfulResult());
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

            return this.Ok(
                new UserDto
                {
                    Id = user.Id,
                    HospitalId = user.HospitalId,
                    RegionId = user.RegionId,
                    UserName = user.UserName,
                    UserType = user.UserType,
                }.MakeSuccessfulResult());
        }
    }
}