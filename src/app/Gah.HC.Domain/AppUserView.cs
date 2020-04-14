namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class AppUserView.
    /// Implements the <see cref="Gah.HC.Domain.Entity{Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Domain.Entity{Guid}" />
    public class AppUserView : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserView"/> class.
        /// </summary>
        public AppUserView()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserView"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AppUserView(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataType(DataType.EmailAddress)]
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public AppUserType UserType { get; set; } = AppUserType.Hospital;

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public Guid? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>The name of the region.</value>
        [MaxLength(50)]
        public string? RegionName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid? HospitalId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hospital.
        /// </summary>
        /// <value>The name of the hospital.</value>
        [MaxLength(200)]
        public string? HospitalName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value><c>true</c> if this instance is approved; otherwise, <c>false</c>.</value>
        [Required]
        public bool IsApproved { get; set; } = false;
    }
}
