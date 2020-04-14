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
    /// Class FindAppUserViewByIdQueryHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindAppUserViewByIdQuery, AppUserView}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindAppUserViewByIdQuery, AppUserView}" />
    public class FindAppUserViewByIdQueryHandler : DomainQueryHandler<FindAppUserViewByIdQuery, AppUserView>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAppUserViewByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindAppUserViewByIdQueryHandler(IHospitalCapacityUow uow, ILogger<FindAppUserViewByIdQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppUserView&gt;.</returns>
        /// <inheritdoc />
        public override Task<AppUserView> Handle(FindAppUserViewByIdQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"Attempting to find user view {request.Id}");

            return this.uow.AppUserViewRepository.FindAsync(request.Id).AsTask();
        }
    }
}
