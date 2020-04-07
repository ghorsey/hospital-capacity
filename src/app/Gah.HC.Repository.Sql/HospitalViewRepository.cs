namespace Gah.HC.Repository.Sql
{
    using System;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalViewRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{HospitalView, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{HospitalView, Guid, HospitalCapacityContext}" />
    public class HospitalViewRepository : RepositoryBase<HospitalView, Guid, HospitalCapacityContext>, IHospitalViewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalViewRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HospitalViewRepository(
            HospitalCapacityContext context,
            ILogger<RepositoryBase<HospitalView, Guid, HospitalCapacityContext>> logger)
            : base(context, logger)
        {
        }
    }
}
