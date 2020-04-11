namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class SetUserPasswordCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandBase" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandBase" />
    public class SetUserPasswordCommand : DomainCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserPasswordCommand" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">
        /// user
        /// or
        /// password.
        /// </exception>
        public SetUserPasswordCommand(AppUser user, string password, string correlationId)
            : base(correlationId)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user));
            this.Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public AppUser User { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; }
    }
}
