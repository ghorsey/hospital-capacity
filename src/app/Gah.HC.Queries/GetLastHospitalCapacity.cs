namespace Gah.HC.Queries
{
    using System;
    using System.Collections.Generic;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <inheritdoc/>
    public class GetLastHospitalCapacity : DomainQueryBase<List<HospitalCapacity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLastHospitalCapacity"/> class.
        /// </summary>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="maxRecentRecords">The maximum recent records.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public GetLastHospitalCapacity(Guid hospitalId, int maxRecentRecords, string correlationId)
            : base(correlationId)
        {
            this.HospitalId = hospitalId;
            this.MaxRecentRecords = maxRecentRecords;
        }

        /// <summary>
        /// Gets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid HospitalId { get; }

        /// <summary>
        /// Gets the maximum recent records.
        /// </summary>
        /// <value>The maximum recent records.</value>
        public int MaxRecentRecords { get; }
    }
}
