namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <inheritdoc/>
    public interface IHospitalCapacityRepository : IRepository<HospitalCapacity, Guid>
    {
        /// <summary>
        /// Gets the recent asynchronous.
        /// </summary>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="last">The last.</param>
        /// <returns>Task&lt;List&lt;HospitalCapacity&gt;&gt;.</returns>
        public Task<List<HospitalCapacity>> GetRecentAsync(Guid hospitalId, int last);
    }
}
