namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;

    /// <summary>
    /// Class RegisterRegionUserCommand.
    /// </summary>
    public class RegisterRegionUserCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRegionUserCommand" /> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">
        /// email
        /// or
        /// password
        /// or
        /// regionName.
        /// </exception>
        public RegisterRegionUserCommand(string email, string password, string regionName, string correlationId)
            : base(correlationId)
        {
            this.Email = email ?? throw new ArgumentNullException(nameof(email));
            this.Password = password ?? throw new ArgumentNullException(nameof(password));
            this.RegionName = regionName ?? throw new ArgumentNullException(nameof(regionName));
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
        /// Gets the name of the region.
        /// </summary>
        /// <value>The name of the region.</value>
        public string RegionName { get; }
    }
}
