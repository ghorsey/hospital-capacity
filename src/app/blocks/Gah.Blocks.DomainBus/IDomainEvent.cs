namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// Interface <c>IEvent</c>
    /// Implements the <see cref="MediatR.INotification" />.
    /// </summary>
    /// <seealso cref="MediatR.INotification" />
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        string EventType { get; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        string CorrelationId { get; }
    }
}
