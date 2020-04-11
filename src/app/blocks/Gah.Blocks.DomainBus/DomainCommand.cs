namespace Gah.Blocks.DomainBus
{
    using System;

    /// <summary>
    /// Class DomainCommandBase.
    /// Implements the <see cref="IDomainCommand" />.
    /// </summary>
    /// <seealso cref="IDomainCommand" />
    public abstract class DomainCommand : IDomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommand"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        protected DomainCommand(string correlationId)
        {
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommand"/> class.
        /// </summary>
        protected DomainCommand()
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
