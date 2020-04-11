namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionsController" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        public RegionsController(IDomainBus domainBus, ILogger<RegionsController> logger)
            : base(logger)
        {
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
        [ProducesResponseType(typeof(List<Result<AppUser>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindRegionUsersAsync(string idOrSlug, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding all the users for the id or slug '{idOrSlug}'");

            var region = await this.domainBus.ExecuteAsync(this.MakeIdOrSlugQuery(idOrSlug));

            var q = new FindAppUsersByRegionOrHospitalQuery(this.HttpContext.TraceIdentifier, region.Id);

            var result = this.domainBus.ExecuteAsync(q, cancellationToken);

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// Makes the identifier or slug query.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <returns>FindRegionByIdOrSlug.</returns>
        private FindRegionByIdOrSlugQuery MakeIdOrSlugQuery(string idOrSlug)
        {
            if (Guid.TryParse(idOrSlug, out var id))
            {
                return new FindRegionByIdOrSlugQuery(this.HttpContext.TraceIdentifier, id);
            }

            return new FindRegionByIdOrSlugQuery(this.HttpContext.TraceIdentifier, slug: idOrSlug);
        }
    }
}
