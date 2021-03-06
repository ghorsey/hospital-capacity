﻿namespace Gah.HC.Repository.Sql
{
    using Gah.HC.Repository;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalCapacityUow.
    /// Implements the <see cref="Gah.HC.Repository.Sql.UnitOfWork{HospitalCapacityContext}" />
    /// Implements the <see cref="Gah.HC.Repository.IHospitalCapacityUow" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.UnitOfWork{HospitalCapacityContext}" />
    /// <seealso cref="Gah.HC.Repository.IHospitalCapacityUow" />
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
            this.RegionRepository = new RegionRepository(context, loggerFactory.CreateLogger<RegionRepository>());
            this.HospitalRepository = new HospitalRepository(context, loggerFactory.CreateLogger<HospitalRepository>());
            this.HospitalCapacityRepository = new HospitalCapacityRepository(context, loggerFactory.CreateLogger<HospitalCapacityRepository>());
            this.HospitalViewRepository = new HospitalViewRepository(context, loggerFactory.CreateLogger<HospitalViewRepository>());
            this.AppUserViewRepository = new AppUserViewRepository(context, loggerFactory.CreateLogger<AppUserViewRepository>());
        }

        /// <summary>
        /// Gets the hospital repository.
        /// </summary>
        /// <value>The hospital repository.</value>
        public IHospitalRepository HospitalRepository { get; }

        /// <summary>
        /// Gets the hospital view repository.
        /// </summary>
        /// <value>The hospital view repository.</value>
        public IHospitalViewRepository HospitalViewRepository { get; }

        /// <summary>
        /// Gets the region repository.
        /// </summary>
        /// <value>The region repository.</value>
        public IRegionRepository RegionRepository { get; }

        /// <summary>
        /// Gets the hospital capacity repository.
        /// </summary>
        /// <value>The hospital capacity repository.</value>
        public IHospitalCapacityRepository HospitalCapacityRepository { get; }

        /// <summary>
        /// Gets the application user view repository.
        /// </summary>
        /// <value>The application user view repository.</value>
        public IAppUserViewRepository AppUserViewRepository { get; }
    }
}
