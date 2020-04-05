﻿namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class MatchRegionByNameHandler : IDomainQueryHandler<MatchRegionByName, List<Region>>
    {
        private readonly ILogger<MatchRegionByNameHandler> logger;
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRegionByNameHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// uow.
        /// </exception>
        public MatchRegionByNameHandler(IHospitalCapacityUow uow, ILogger<MatchRegionByNameHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <inheritdoc/>
        public Task<List<Region>> Handle(MatchRegionByName request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.logger.LogInformation($"handling match by name request for {request.PartialName}");

            return this.uow.RegionRepository.MatchByName(request.PartialName, cancellationToken);
        }
    }
}