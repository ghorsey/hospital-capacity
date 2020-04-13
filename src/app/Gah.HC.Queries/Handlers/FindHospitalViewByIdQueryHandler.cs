namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class FindHospitalViewByIdQueryHandler.
    /// Implements the <see cref="DomainQueryHandler{FindHospitalViewByIdQuery, HospitalView}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindHospitalViewByIdQuery, HospitalView}" />
    public class FindHospitalViewByIdQueryHandler : DomainQueryHandler<FindHospitalViewByIdQuery, HospitalView>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalViewByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindHospitalViewByIdQueryHandler(
            IHospitalCapacityUow uow,
            ILogger<FindHospitalViewByIdQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;HospitalView&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override Task<HospitalView> Handle(FindHospitalViewByIdQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"Finding the hospital view for {request.Id}");

            return this.uow.HospitalViewRepository.FindAsync(request.Id, cancellationToken).AsTask();
        }
    }
}
