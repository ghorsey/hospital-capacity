namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class ChangeUserPasswordCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandBase" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandBase" />
    public class ChangeUserPasswordCommand : DomainCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserPasswordCommand" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">
        /// user
        /// or
        /// user
        /// or
        /// newPassword.
        /// </exception>
        public ChangeUserPasswordCommand(AppUser user, string currentPassword, string newPassword, string correlationId)
            : base(correlationId)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user));
            this.CurrentPassword = currentPassword ?? throw new ArgumentNullException(nameof(user));
            this.NewPassword = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public AppUser User { get; }

        /// <summary>
        /// Gets the current password.
        /// </summary>
        /// <value>The current password.</value>
        public string CurrentPassword { get; }

        /// <summary>
        /// Gets the new password.
        /// </summary>
        /// <value>The new password.</value>
        public string NewPassword { get; }
    }
}
