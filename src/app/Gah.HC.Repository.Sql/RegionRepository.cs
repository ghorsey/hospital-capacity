namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegionRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{Region, Guid, HospitalCapacityContext}" />
    /// Implements the <see cref="Gah.HC.Repository.IRegionRepository" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{Region, Guid, HospitalCapacityContext}" />
    /// <seealso cref="Gah.HC.Repository.IRegionRepository" />
    public class RegionRepository : RepositoryBase<Region, Guid, HospitalCapacityContext>, IRegionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public RegionRepository(HospitalCapacityContext context, ILogger<RegionRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Matches the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;Region&gt;&gt;.</returns>
        public Task<List<Region>> MatchByName(string name, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Searching for a region named '{name}'");

            return this.Entities.Where(e => EF.Functions.Like(e.Name, $"%{name}%")).ToListAsync(cancellationToken);
        }
    }
}
