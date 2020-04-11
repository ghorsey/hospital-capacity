namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class SetUserAuthorizedCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommand" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommand" />
    public class SetUserAuthorizedCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserAuthorizedCommand"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isAuthorized">if set to <c>true</c> [is authorized].</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public SetUserAuthorizedCommand(AppUser user, bool isAuthorized, string correlationId)
            : base(correlationId)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user));
            this.IsAuthorized = isAuthorized;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public AppUser User { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is authorized.
        /// </summary>
        /// <value><c>true</c> if this instance is authorized; otherwise, <c>false</c>.</value>
        public bool IsAuthorized { get; }
    }
}
