namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class HospitalCapacity.
    /// Implements the <see cref="Gah.HC.Domain.Entity{Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Domain.Entity{Guid}" />
    public class HospitalCapacity : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalCapacity"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public HospitalCapacity(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalCapacity"/> class.
        /// </summary>
        public HospitalCapacity()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Gets or sets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid HospitalId { get; set; }

        /// <summary>
        /// Gets or sets the hospital.
        /// </summary>
        /// <value>The hospital.</value>
        public Hospital? Hospital { get; set; }

        /// <summary>
        /// Gets or sets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        [Required]
        [Range(1, int.MaxValue)]
        public int BedCapacity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        [Required]
        [Range(1, int.MaxValue)]
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

        /// <summary>
        /// Calculates the percentage available.
        /// </summary>
        public void CalculatePercentageAvailable()
        {
            this.PercentOfUsage = (int)Math.Round((100 / (decimal)this.BedCapacity) * this.BedsInUse);
        }
    }
}
