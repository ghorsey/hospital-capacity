namespace Gah.Blocks.DomainBus
{
    using System.Reflection;

    /// <summary>
    /// Class DomainEventBase.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainEvent" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainEvent" />
    public class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        public DomainEvent(string correlationId)
        {
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType => $"{this.GetType().GetTypeInfo().FullName}, {this.GetType().Assembly.GetName().Name}";

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; }
    }
}
