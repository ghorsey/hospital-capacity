namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class HospitalCapacityRepository : RepositoryBase<HospitalCapacity, Guid, HospitalCapacityContext>, IHospitalCapacityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalCapacityRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HospitalCapacityRepository(HospitalCapacityContext context, ILogger<HospitalCapacityRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Gets the recent10 asynchronous.
        /// </summary>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="last">The last.</param>
        /// <returns>Task&lt;List&lt;HospitalCapacity&gt;&gt;.</returns>
        public Task<List<HospitalCapacity>> GetRecentAsync(Guid hospitalId, int last)
        {
            this.Logger.LogInformation($"Fetching recent 10 for {hospitalId}");

            return this.Entities.Where(e => e.HospitalId == hospitalId)
                .OrderBy(e => e.CreatedOn)
                .Take(last)
                .ToListAsync();
        }
    }
}
