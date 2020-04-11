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
    public class FindHospitalsQueryHandler : DomainQueryHandler<FindHospitalsQuery, List<HospitalView>>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalsQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindHospitalsQueryHandler(IHospitalCapacityUow uow, ILogger<FindHospitalsQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;Hospital&gt;&gt;.</returns>
        /// <inheritdoc />
        public override Task<List<HospitalView>> Handle(FindHospitalsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            this.Logger.LogInformation("Finding hospitals");

            return this.uow.HospitalViewRepository.FindHospitalsAsync(
                request.RegionId,
                request.Name,
                request.City,
                request.State,
                request.PostalCode,
                request.BedCapacity,
                request.BedsInUse,
                request.PercentOfUse,
                request.IsCovid);
        }
    }
}
