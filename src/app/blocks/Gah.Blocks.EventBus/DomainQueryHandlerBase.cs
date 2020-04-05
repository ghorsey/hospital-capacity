namespace Gah.Blocks.EventBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class DomainQueryHandlerBase.
    /// Implements the <see cref="Gah.Blocks.EventBus.IDomainQueryHandler{TQuery, TResponse}" />.
    /// </summary>
    /// <typeparam name="TQuery">The type of the t query.</typeparam>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="Gah.Blocks.EventBus.IDomainQueryHandler{TQuery, TResponse}" />
    public abstract class DomainQueryHandlerBase<TQuery, TResponse> : IDomainQueryHandler<TQuery, TResponse>
        where TQuery : IDomainQuery<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainQueryHandlerBase{TQuery, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public DomainQueryHandlerBase(ILogger logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }

        /// <inheritdoc/>
        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
