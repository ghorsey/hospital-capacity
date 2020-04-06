namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class FindBySlugOrIdQueryHandler.
    /// Implements the <see cref="Gah.Blocks.EventBus.DomainQueryHandlerBase{FindBySlugOrIdQuery, Hospital}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.DomainQueryHandlerBase{FindBySlugOrIdQuery, Hospital}" />
    public class FindBySlugOrIdQueryHandler : DomainQueryHandlerBase<FindBySlugOrIdQuery, Hospital>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindBySlugOrIdQueryHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public FindBySlugOrIdQueryHandler(IHospitalCapacityUow uow, ILogger<FindBySlugOrIdQueryHandler> logger)
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
        public override async Task<Hospital> Handle(FindBySlugOrIdQuery request, CancellationToken cancellationToken)
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
