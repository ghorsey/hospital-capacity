namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Hospital.
    /// Implements the <see cref="Entity{Guid}" />.
    /// </summary>
    /// <seealso cref="Entity{Guid}" />
    public class Hospital : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hospital"/> class.
        /// </summary>
        public Hospital()
            : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hospital"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Hospital(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        [Required]
        [MaxLength(100)]
        public string Address1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        [MaxLength(100)]
        public string Address2 { get; set;  } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [Required]
        [MaxLength(2)]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

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
        public int PercentageAvailable { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>The last updated.</value>
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the URL slug.
        /// </summary>
        /// <value>The URL slug.</value>
        [Required]
        [MaxLength(402)]
        public string Slug
        {
            get => $"{this.Name} {this.Address1} {this.City} {this.State}".ToSlug();
            set
            {
                // no op
            }
        }

        /// <summary>
        /// Calculates the percentage available.
        /// </summary>
        public void CalculatePercentageAvailable()
        {
            this.PercentageAvailable = (int)Math.Round((100 / (decimal)this.BedCapacity) * this.BedsInUse);
        }
    }
}
