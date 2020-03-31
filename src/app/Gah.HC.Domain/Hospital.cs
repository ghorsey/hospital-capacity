namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Hospital.
    /// </summary>
    public class Hospital
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        [Required]
        public string Address1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        public string Address2 { get; set;  } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [Required]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [Required]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        [Required]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        [Required, Range(1, int.MaxValue)]
        public int BedCapacity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        [Required, Range(1, int.MaxValue)]
        public int BedsInUse { get; set; } = 1;

        /// <summary>
        /// Gets or sets the percentage available.
        /// </summary>
        /// <value>The percentage available.</value>
        public int PercentageAvailable { get; set; }

        /// <summary>
        /// Calculates the percentage available.
        /// </summary>
        public void CalculatePercentageAvailable()
        {
            this.PercentageAvailable = (int)Math.Round((100 / (decimal)this.BedCapacity) * this.BedsInUse);
        }
    }
}
