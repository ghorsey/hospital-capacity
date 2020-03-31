namespace Gah.HC.Repository.Sql
{
    using System;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AppUserRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{AppUser, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{AppUser, Guid, HospitalCapacityContext}" />
    public class AppUserRepository : RepositoryBase<AppUser, Guid, HospitalCapacityContext>, IAppUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public AppUserRepository(HospitalCapacityContext context, ILogger<AppUserRepository> logger)
            : base(context, logger)
        {
        }
    }
}
