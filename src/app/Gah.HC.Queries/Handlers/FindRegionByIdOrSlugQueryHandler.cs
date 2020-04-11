namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class FindRegionByIdOrSlugHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindRegionByIdOrSlug, Region}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindRegionByIdOrSlug, Region}" />
    public class FindRegionByIdOrSlugQueryHandler : DomainQueryHandlerBase<FindRegionByIdOrSlugQuery, Region>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindRegionByIdOrSlugQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindRegionByIdOrSlugQueryHandler(IHospitalCapacityUow uow, ILogger logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Region&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override Task<Region> Handle(FindRegionByIdOrSlugQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            this.Logger.LogInformation($"Finding regions by id '{request.Id}' or slug '{request.Slug}'");

            if (request.Id != null)
            {
                return this.uow.RegionRepository.FindAsync(request.Id.Value, cancellationToken).AsTask();
            }

            return this.uow.RegionRepository.FindBySlug(request.Slug, cancellationToken);
        }
    }
}
