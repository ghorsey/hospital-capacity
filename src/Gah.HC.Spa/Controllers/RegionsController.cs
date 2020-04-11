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
    using Gah.HC.Spa.Models.Authorization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegionsController.
    /// Implements the <see cref="Gah.HC.Spa.Controllers.BaseController" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Spa.Controllers.BaseController" />
    [ApiController]
    [Route("api/regions")]
    public class RegionsController : BaseController
    {
        private readonly IDomainBus domainBus;
        private readonly IAuthorizationService authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionsController" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="authorizationService">The authorization service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// domainBus.
        /// </exception>
        public RegionsController(IDomainBus domainBus, IAuthorizationService authorizationService, ILogger<RegionsController> logger)
            : base(logger)
        {
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        }

        /// <summary>
        /// Finds the partial name of the regions by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<List<Region>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindRegionsByPartialNameAsync([FromQuery] string name, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding regions named {name}");

            var query = new MatchRegionByNameQuery(name, this.HttpContext.TraceIdentifier);

            var response = await this.domainBus.ExecuteAsync(query, cancellationToken);

            return this.Ok(response.MakeSuccessfulResult());
        }

        /// <summary>
        /// Finds the region users.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{idOrSlug}/users")]
        [ProducesResponseType(typeof(List<Result<UserDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindRegionUsersAsync(string idOrSlug, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding all the users for the id or slug '{idOrSlug}'");

            var region = await this.domainBus.ExecuteAsync(this.domainBus.MakeFindRegionIdOrSlugQuery(idOrSlug, this.HttpContext.TraceIdentifier));

            var authResponse = await this.authorizationService.AuthorizeAsync(this.User, region, new ViewRegionUsersRequirement());

            if (!authResponse.Succeeded)
            {
                return this.Forbid();
            }

            var q = new FindAppUsersByRegionOrHospitalQuery(this.HttpContext.TraceIdentifier, region.Id);

            var result = await this.domainBus.ExecuteAsync(q, cancellationToken);

            return this.Ok(
                result.Select(
                    r => new UserDto
                    {
                        HospitalId = r.HospitalId,
                        RegionId = r.RegionId,
                        UserName = r.UserName,
                        UserType = r.UserType,
                    }).MakeSuccessfulResult());
        }
    }
}
