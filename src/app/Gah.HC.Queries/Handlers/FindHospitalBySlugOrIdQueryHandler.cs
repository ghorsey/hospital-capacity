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
    /// Class FindBySlugOrIdQueryHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindBySlugOrIdQuery, Hospital}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindBySlugOrIdQuery, Hospital}" />
    public class FindHospitalBySlugOrIdQueryHandler : DomainQueryHandler<FindHospitalBySlugOrIdQuery, Hospital>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalBySlugOrIdQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindHospitalBySlugOrIdQueryHandler(IHospitalCapacityUow uow, ILogger<FindHospitalBySlugOrIdQueryHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Hospital&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override async Task<Hospital> Handle(FindHospitalBySlugOrIdQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"Finding hospital with id: {request.Id} or slug: {request.Slug}");

            if (request.Id != null && request.Id != Guid.Empty)
            {
                return await this.uow.HospitalRepository.FindAsync(request.Id.Value).ConfigureAwait(false);
            }

            return await this.uow.HospitalRepository.FindBySlugAsync(request.Slug, cancellationToken).ConfigureAwait(false);
        }
    }
}
