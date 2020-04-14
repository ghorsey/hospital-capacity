namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class FindAppUserViewsByRegionOrHospitalQueryHandler : DomainQueryHandler<FindAppUserViewsByRegionOrHospitalQuery, List<AppUserView>>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAppUserViewsByRegionOrHospitalQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindAppUserViewsByRegionOrHospitalQueryHandler(IHospitalCapacityUow uow, ILogger<FindAppUserViewsByRegionOrHospitalQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;AppUserView&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override Task<List<AppUserView>> Handle(FindAppUserViewsByRegionOrHospitalQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"About to find app user views for region {request.RegionId} or hospital {request.HospitalId}");

            return this.uow.AppUserViewRepository.FindByAsync(request.RegionId, request.HospitalId, cancellationToken);
        }
    }
}
