namespace Gah.HC.Queries
{
    using System;
    using System.Collections.Generic;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <inheritdoc/>
    public class FindHospitalsQuery : DomainQuery<List<HospitalView>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalsQuery" /> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="bedCapacity">The bed capacity.</param>
        /// <param name="bedsInUse">The beds in use.</param>
        /// <param name="percentageAvailable">The percentage available.</param>
        /// <param name="isCovid">if set to <c>true</c> [is covid].</param>
        public FindHospitalsQuery(
            string correlationId,
            Guid? regionId = null,
            string name = "",
            string city = "",
            string state = "",
            string postalCode = "",
            int bedCapacity = 0,
            int bedsInUse = -1,
            int percentageAvailable = -1,
            bool? isCovid = null)
            : base(correlationId)
        {
            this.RegionId = regionId;
            this.Name = name;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.BedCapacity = bedCapacity;
            this.BedsInUse = bedsInUse;
            this.PercentOfUse = percentageAvailable;
            this.IsCovid = isCovid;
        }

        /// <summary>
        /// Gets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public Guid? RegionId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; }

        /// <summary>
        /// Gets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; }

        /// <summary>
        /// Gets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        public int BedCapacity { get; }

        /// <summary>
        /// Gets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        public int BedsInUse { get; }

        /// <summary>
        /// Gets the percentage available.
        /// </summary>
        /// <value>The percentage available.</value>
        public int PercentOfUse { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is covid.
        /// </summary>
        /// <value><c>null</c> if [is covid] contains no value, <c>true</c> if [is covid]; otherwise, <c>false</c>.</value>
        public bool? IsCovid { get; }
    }
}
