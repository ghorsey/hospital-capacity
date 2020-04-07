namespace Gah.HC.Spa.Models.Hospitals
{
    using System;

    /// <summary>
    /// Class RapidHospitalUpdateInput.
    /// </summary>
    public class RapidHospitalUpdateInput
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        public int BedsInUse { get; }

        /// <summary>
        /// Gets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        public int BedCapacity { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is covid.
        /// </summary>
        /// <value><c>true</c> if this instance is covid; otherwise, <c>false</c>.</value>
        public bool IsCovid { get; }
    }
}
