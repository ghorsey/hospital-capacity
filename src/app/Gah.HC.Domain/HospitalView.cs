namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class HospitalView.
    /// Implements the <see cref="Gah.HC.Domain.Entity{Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Domain.Entity{Guid}" />
    public class HospitalView : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalView"/> class.
        /// </summary>
        public HospitalView()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalView"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public HospitalView(Guid id)
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
        public string Address2 { get; set; } = string.Empty;

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
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is covid.
        /// </summary>
        /// <value><c>true</c> if this instance is covid; otherwise, <c>false</c>.</value>
        [Required]
        public bool IsCovid { get; set; }

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
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        [Required]
        public Guid RegionId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The state.</value>
        [Required]
        [MaxLength(50)]
        public string RegionName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the capacity1.
        /// </summary>
        /// <value>The capacity1.</value>
        [Required]
        public int Capacity1 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used1.
        /// </summary>
        /// <value>The used1.</value>
        [Required]
        public int Used1 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity2.
        /// </summary>
        /// <value>The capacity2.</value>
        [Required]
        public int Capacity2 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used2.
        /// </summary>
        /// <value>The used2.</value>
        [Required]
        public int Used2 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity3.
        /// </summary>
        /// <value>The capacity3.</value>
        [Required]
        public int Capacity3 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used3.
        /// </summary>
        /// <value>The used3.</value>
        [Required]
        public int Used3 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity4.
        /// </summary>
        /// <value>The capacity4.</value>
        [Required]
        public int Capacity4 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used4.
        /// </summary>
        /// <value>The used4.</value>
        [Required]
        public int Used4 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity5.
        /// </summary>
        /// <value>The capacity5.</value>
        [Required]
        public int Capacity5 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used5.
        /// </summary>
        /// <value>The used5.</value>
        [Required]
        public int Used5 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity6.
        /// </summary>
        /// <value>The capacity6.</value>
        [Required]
        public int Capacity6 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used6.
        /// </summary>
        /// <value>The used6.</value>
        [Required]
        public int Used6 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity7.
        /// </summary>
        /// <value>The capacity7.</value>
        [Required]
        public int Capacity7 { get; set; }

        /// <summary>
        /// Gets or sets the used7.
        /// </summary>
        /// <value>The used7.</value>
        [Required]
        public int Used7 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity8.
        /// </summary>
        /// <value>The capacity8.</value>
        [Required]
        public int Capacity8 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used8.
        /// </summary>
        /// <value>The used8.</value>
        [Required]
        public int Used8 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity9.
        /// </summary>
        /// <value>The capacity9.</value>
        [Required]
        public int Capacity9 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used9.
        /// </summary>
        /// <value>The used9.</value>
        [Required]
        public int Used9 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the capacity10.
        /// </summary>
        /// <value>The capacity10.</value>
        [Required]
        public int Capacity10 { get; set; } = 0;

        /// <summary>
        /// Gets or sets the used10.
        /// </summary>
        /// <value>The used10.</value>
        [Required]
        public int? Used10 { get; set; }
    }
}
