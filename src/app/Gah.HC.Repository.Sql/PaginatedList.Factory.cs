namespace Gah.HC.Repository.Sql
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class PaginatedList.
    /// </summary>
    public static class PaginatedListFactory
    {
        /// <summary>
        /// Makes the paginated list.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="size">The size.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;PaginatedList&lt;T&gt;&gt;.</returns>
        public static async Task<PaginatedList<T>> MakePaginatedList<T>(this IQueryable<T> query, int page, int size, CancellationToken cancellationToken = default)
            where T : class
        {
            var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await query
                .Take(size)
                .Skip(size * (page - 1))
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return items.MakePaginatedList(count, page, size);
        }
    }
}
