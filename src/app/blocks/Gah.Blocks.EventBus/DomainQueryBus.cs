namespace TS.Blocks.DomainQueries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>QueryBus</c>.
    /// </summary>
    public class DomainQueryBus : IDomainQueryBus
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<DomainQueryBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainQueryBus"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public DomainQueryBus(IMediator mediator, ILogger<DomainQueryBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;TResponse&gt;</c>.</returns>
        public Task<TResponse> ExecuteAsync<TResponse>(IDomainQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            this.logger.LogDebug("Executing query {@query}", query);

            return this.mediator.Send(query, cancellationToken);
        }
    }
}
