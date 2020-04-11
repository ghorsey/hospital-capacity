namespace Gah.Blocks.DomainBus
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class DomainQueryBase.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainQuery{TResponse}" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainQuery{TResponse}" />
    public abstract class DomainQuery<TResponse> : IDomainQuery<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainQuery{TResponse}"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">correlationId.</exception>
        public DomainQuery(string correlationId)
        {
            this.CorrelationId = correlationId ?? throw new ArgumentNullException(nameof(correlationId));
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; }
    }
}
