namespace Gah.HC.Spa.Models.Hospitals
{
    using System;

    /// <summary>
    /// Class HospitalCapacityDto.
    /// </summary>
    public class HospitalCapacityDto
    {
        /// <summary>
        /// Gets or sets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid HospitalId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        public int BedCapacity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        public int BedsInUse { get; set; } = 1;

        /// <summary>
        /// Gets or sets the percentage available.
        /// </summary>
        /// <value>The percentage available.</value>
        public int PercentOfUsage { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
