namespace Gah.HC.Repository.Sql
{
    using System;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
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
    }
}
