namespace Gah.Blocks.EventBus
{
    using System;

    /// <summary>
    /// Class DomainCommandBase.
    /// Implements the <see cref="IDomainCommand" />.
    /// </summary>
    /// <seealso cref="IDomainCommand" />
    public abstract class DomainCommandBase : IDomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommandBase"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        protected DomainCommandBase(string correlationId)
        {
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommandBase"/> class.
        /// </summary>
        protected DomainCommandBase()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; }
    }
}
