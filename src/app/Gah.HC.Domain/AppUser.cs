namespace Gah.HC.Domain
{
    using System;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Class AppUser.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.IdentityUser{Guid}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser{Guid}" />
    public class AppUser : IdentityUser
    {
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
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
        public Region? Region { get; set; }

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
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value><c>true</c> if this instance is approved; otherwise, <c>false</c>.</value>
        public bool IsApproved { get; set; } = false;
    }
}
