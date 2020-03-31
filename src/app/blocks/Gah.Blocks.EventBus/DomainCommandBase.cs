namespace TS.Blocks.DomainCommands
{
    using System;

    /// <summary>
    /// Class DomainCommandBase.
    /// Implements the <see cref="TS.Blocks.DomainCommands.IDomainCommand" />.
    /// </summary>
    /// <seealso cref="TS.Blocks.DomainCommands.IDomainCommand" />
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
