namespace Gah.HC.Repository.Sql
{
    using System;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{Hospital, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{Hospital, Guid, HospitalCapacityContext}" />
    public class HospitalRepository : RepositoryBase<Hospital, Guid, HospitalCapacityContext>, IHospitalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HospitalRepository(HospitalCapacityContext context, ILogger<HospitalRepository> logger)
            : base(context, logger)
        {
        }
    }
}
