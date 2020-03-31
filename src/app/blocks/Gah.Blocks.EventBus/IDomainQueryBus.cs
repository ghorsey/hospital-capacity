namespace TS.Blocks.DomainQueries
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface <c>IQueryBus</c>.
    /// </summary>
    public interface IDomainQueryBus
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;TResponse&gt;</c>.</returns>
        Task<TResponse> ExecuteAsync<TResponse>(IDomainQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}
