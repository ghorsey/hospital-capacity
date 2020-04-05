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
        public Task<List<Region>> MatchByName(string name, CancellationToken cancellationToken);
    }
}
