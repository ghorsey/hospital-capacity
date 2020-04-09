namespace Gah.HC.Spa.Models.Authorization
{
    using System;
    using Gah.HC.Domain;

    /// <summary>
    /// Class UserDto.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public AppUserType UserType { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>The user name.</value>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public Guid? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid? HospitalId { get; set; }
    }
}
