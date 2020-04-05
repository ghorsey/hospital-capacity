namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
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
        public async Task<IActionResult> FindRegionsByPartialNameAsync([FromQuery] string name, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding regions named {name}");

            var query = new MatchRegionByName(name);

            var response = await this.domainBus.ExecuteAsync(query, cancellationToken);

            return this.Ok(response.MakeSuccessfulResult());
        }
    }
}
