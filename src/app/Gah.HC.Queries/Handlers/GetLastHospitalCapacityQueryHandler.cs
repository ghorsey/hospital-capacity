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
    public class GetLastHospitalCapacityQueryHandler : DomainQueryHandler<GetLastHospitalCapacityQuery, List<HospitalCapacity>>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLastHospitalCapacityQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        public GetLastHospitalCapacityQueryHandler(IHospitalCapacityUow uow, ILogger<GetLastHospitalCapacityQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;GetLastHospitalCapacity&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override Task<List<HospitalCapacity>> Handle(
            GetLastHospitalCapacityQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"Finding {request.MaxRecentRecords} most recent capacity objects for {request.HospitalId}");

            return this.uow.HospitalCapacityRepository.GetRecentAsync(request.HospitalId, request.MaxRecentRecords);
        }
    }
}
