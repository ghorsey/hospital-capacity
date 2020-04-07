namespace Gah.Blocks.DomainBus
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IDomainBus.
    /// </summary>
    public interface IDomainBus
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;TResponse&gt;</c>.</returns>
        Task<TResponse> ExecuteAsync<TResponse>(IDomainQuery<TResponse> query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : IDomainCommand;

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task PublishAsync<TEvent>(
            CancellationToken cancellationToken = default,
            params TEvent[] events)
            where TEvent : IDomainEvent;
    }
}
