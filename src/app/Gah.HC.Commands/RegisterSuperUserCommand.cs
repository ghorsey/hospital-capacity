namespace Gah.HC.Commands
{
    using Gah.Blocks.DomainBus;

    /// <summary>
    /// Class RegisterSuperUserCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommand" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommand" />
    public class RegisterSuperUserCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterSuperUserCommand"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public RegisterSuperUserCommand(string email, string password, string correlationId)
            : base(correlationId)
        {
            this.Email = email;
            this.Password = password;
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
    }
}
