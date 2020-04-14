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
    /// Class AppUserViewRepository.
    /// Implements the <see cref="RepositoryBase{AppUserView, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="RepositoryBase{AppUserView, Guid, HospitalCapacityContext}" />
    public class AppUserViewRepository : RepositoryBase<AppUserView, Guid, HospitalCapacityContext>, IAppUserViewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserViewRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public AppUserViewRepository(HospitalCapacityContext context, ILogger<AppUserViewRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Finds the by asynchronous.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;AppUserView&gt;&gt;.</returns>
        public Task<List<AppUserView>> FindByAsync(Guid? regionId = null, Guid? hospitalId = null, CancellationToken cancellationToken = default)
        {
            var q = this.Entities.AsQueryable();

            if (regionId.HasValue)
            {
                q = q.Where(e => e.RegionId == regionId);
            }

            if (hospitalId.HasValue)
            {
                q = q.Where(e => e.HospitalId == hospitalId);
            }

            return q.OrderBy(e => e.UserName)
                .ThenBy(e => e.RegionName)
                .ThenBy(e => e.HospitalName)
                .ToListAsync();
        }
    }
}
