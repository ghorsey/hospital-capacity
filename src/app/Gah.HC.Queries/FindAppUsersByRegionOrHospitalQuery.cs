namespace Gah.HC.Queries
{
    using System;
    using System.Collections.Generic;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <inheritdoc/>
    public class FindAppUsersByRegionOrHospitalQuery : DomainQuery<List<AppUser>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindAppUsersByRegionOrHospitalQuery" /> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="hosptialId">The hosptial identifier.</param>
        public FindAppUsersByRegionOrHospitalQuery(string correlationId, Guid? regionId = null, Guid? hosptialId = null)
            : base(correlationId)
        {
            this.RegionId = regionId;
            this.HospitalId = hosptialId;
        }

        /// <summary>
        /// Gets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid? HospitalId { get; }

        /// <summary>
        /// Gets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public Guid? RegionId { get; }
    }
}
