namespace Gah.HC.Repository.Sql
{
    using Gah.HC.Repository;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalCapacityUow.
    /// Implements the <see cref="Gah.HC.Repository.Sql.UnitOfWork{HospitalCapacityContext}" />
    /// Implements the <see cref="Gah.HC.Repository.Sql.IHospitalCapacityUow" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.UnitOfWork{HospitalCapacityContext}" />
    /// <seealso cref="Gah.HC.Repository.Sql.IHospitalCapacityUow" />
    public class HospitalCapacityUow : UnitOfWork<HospitalCapacityContext>, IHospitalCapacityUow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalCapacityUow"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public HospitalCapacityUow(HospitalCapacityContext context, ILoggerFactory loggerFactory)
            : base(context, loggerFactory)
        {
            this.HospitalRepository = new HospitalRepository(context, loggerFactory.CreateLogger<HospitalRepository>());
            this.AppUserRepository = new AppUserRepository(context, loggerFactory.CreateLogger<AppUserRepository>());
        }

        /// <summary>
        /// Gets the hospital repository.
        /// </summary>
        /// <value>The hospital repository.</value>
        public IHospitalRepository HospitalRepository { get; }

        /// <summary>
        /// Gets the application user repository.
        /// </summary>
        /// <value>The application user repository.</value>
        public IAppUserRepository AppUserRepository { get; }
    }
}
