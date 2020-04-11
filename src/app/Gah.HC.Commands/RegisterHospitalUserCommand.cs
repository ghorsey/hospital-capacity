namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;

    /// <summary>
    /// Class RegisterHospitalUserCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommand" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommand" />
    public class RegisterHospitalUserCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHospitalUserCommand" /> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        public RegisterHospitalUserCommand(string email, string password, Guid hospitalId, Guid regionId)
        {
            this.Email = email;
            this.Password = password;
            this.HospitalId = hospitalId;
            this.RegionId = regionId;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; }

        /// <summary>
        /// Gets the hospital identifier.
        /// </summary>
        /// <value>The hospital identifier.</value>
        public Guid HospitalId { get; }

        /// <summary>
        /// Gets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public Guid RegionId { get; }
    }
}
