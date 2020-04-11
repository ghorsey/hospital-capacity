namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IRegionRepository
    /// Implements the <see cref="Gah.HC.Repository.IRepository{Region, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{Region, Guid}" />
    public interface IRegionRepository : IRepository<Region, Guid>
    {
        /// <summary>
        /// Matches the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;Region&gt;&gt;.</returns>
        public Task<List<Region>> MatchByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Finds the by slug.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Region&gt;.</returns>
        public Task<Region> FindBySlugAsync(string slug, CancellationToken cancellationToken);

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Region&gt;.</returns>
        public Task<Region> FindByNameAsync(string name, CancellationToken cancellationToken);
    }
}
