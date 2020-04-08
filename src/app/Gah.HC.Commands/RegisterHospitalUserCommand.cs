namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;

    /// <summary>
    /// Class RegisterHospitalUserCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandBase" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandBase" />
    public class RegisterHospitalUserCommand : DomainCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHospitalUserCommand"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        public RegisterHospitalUserCommand(string email, string password, Guid hospitalId)
        {
            this.Email = email;
            this.Password = password;
            this.HospitalId = hospitalId;
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
    }
}
