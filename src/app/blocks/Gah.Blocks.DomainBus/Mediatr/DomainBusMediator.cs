namespace Gah.Blocks.DomainBus.Mediatr
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class DomainBus.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainBus" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainBus" />
    public class DomainBusMediator : IDomainBus
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<DomainBusMediator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBusMediator"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public DomainBusMediator(IMediator mediator, ILogger<DomainBusMediator> logger)
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

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="events">The events.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        [DebuggerStepThrough]
        public async Task PublishAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default)
            where TEvent : IDomainEvent
        {
            var eventList = events.ToList();
            if (eventList.Count == 0)
            {
                throw new ArgumentException("Must publish at least one event", nameof(events));
            }

            this.logger.LogDebug("Raising {count} events", eventList.Count);
            var tasks = new List<Task>();
            foreach (var @event in eventList)
            {
                this.logger.LogDebug("Raising event {@event}", @event);
                tasks.Add(this.mediator.Publish(@event, cancellationToken));
            }

            await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        [DebuggerStepThrough]
        public Task PublishAsync<TEvent>(
            CancellationToken cancellationToken = default,
            params TEvent[] events)
            where TEvent : IDomainEvent
        {
            if (events.Length == 0)
            {
                throw new ArgumentException("Must publish at least one event", nameof(events));
            }

            return this.PublishAsync(events.AsEnumerable(), cancellationToken);
        }
    }
}
