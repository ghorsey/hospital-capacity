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
    public class SetUserIsApprovedCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserIsApprovedCommand" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isApproved">if set to <c>true</c> if the user is approved.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">user.</exception>
        public SetUserIsApprovedCommand(AppUser user, bool isApproved, string correlationId)
            : base(correlationId)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user));
            this.IsApproved = isApproved;
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
        public bool IsApproved { get; }
    }
}
