namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class AppUser.
    /// </summary>
    public class AppUser : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class.
        /// </summary>
        public AppUser()
            : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AppUser(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>The password hash.</value>
        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is site admin.
        /// </summary>
        /// <value><c>true</c> if this instance is site admin; otherwise, <c>false</c>.</value>
        [Required]
        public bool IsSiteAdmin { get; set; } = false;

        /// <summary>
        /// Gets or sets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid? HospitalId { get; set; }

        /// <summary>
        /// Gets or sets the hospital.
        /// </summary>
        /// <value>The hospital.</value>
        public Hospital? Hospital { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>The updated on.</value>
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
