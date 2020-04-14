namespace Gah.HC.Events
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class AppUserUpdatedEvent.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainEvent" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainEvent" />
    public class AppUserUpdatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserUpdatedEvent"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">user.</exception>
        public AppUserUpdatedEvent(AppUser user, string correlationId)
            : base(correlationId)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public AppUser User { get; }
    }
}
