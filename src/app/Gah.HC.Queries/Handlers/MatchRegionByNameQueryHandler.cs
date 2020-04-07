namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class MatchRegionByNameQueryHandler : DomainQueryHandlerBase<MatchRegionByNameQuery, List<Region>>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRegionByNameQueryHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger
        /// or
        /// uow.</exception>
        public MatchRegionByNameQueryHandler(IHospitalCapacityUow uow, ILogger<MatchRegionByNameQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <inheritdoc/>
        public override Task<List<Region>> Handle(MatchRegionByNameQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"handling match by name request for {request.PartialName}");

            return this.uow.RegionRepository.MatchByName(request.PartialName, cancellationToken);
        }
    }
}
