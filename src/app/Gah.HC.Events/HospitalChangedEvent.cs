namespace Gah.HC.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class HospitalChangedEvent.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainEventBase" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainEventBase" />
    public class HospitalChangedEvent : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalChangedEvent"/> class.
        /// </summary>
        /// <param name="hospital">The hospital.</param>
        /// <param name="region">The region.</param>
        /// <param name="capacity">The capacity.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">
        /// hospital
        /// or
        /// region
        /// or
        /// capacity.
        /// </exception>
        public HospitalChangedEvent(
            Hospital hospital,
            Region region,
            List<HospitalCapacity> capacity,
            string correlationId)
            : base(correlationId)
        {
            this.Hospital = hospital ?? throw new ArgumentNullException(nameof(hospital));
            this.Region = region ?? throw new ArgumentNullException(nameof(region));
            this.Capacity = capacity ?? throw new ArgumentNullException(nameof(capacity));
        }

        /// <summary>
        /// Gets the hospital.
        /// </summary>
        /// <value>The hospital.</value>
        public Hospital Hospital { get; }

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <value>The region.</value>
        public Region Region { get; }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public List<HospitalCapacity> Capacity { get; }
    }
}
