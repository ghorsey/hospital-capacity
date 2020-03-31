namespace Gah.Blocks.EventBus
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
    }
}
