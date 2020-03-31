namespace Gah.Blocks.EventBus.Mediatr
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class DomainBus.
    /// Implements the <see cref="Gah.Blocks.EventBus.IDomainBus" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.IDomainBus" />
    public class DomainBus : IDomainBus
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<DomainBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBus"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public DomainBus(IMediator mediator, ILogger<DomainBus> logger)
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
        [DebuggerStepThrough]
        public Task<TResponse> ExecuteAsync<TResponse>(IDomainQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            this.logger.LogDebug("Executing query {@query}", query);
            return this.mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        [DebuggerStepThrough]
        public Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : IDomainCommand
        {
            this.logger.LogDebug("Executing {@command}", command);
            return this.mediator.Send(command, cancellationToken);
        }
    }
}
