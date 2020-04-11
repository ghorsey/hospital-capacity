namespace Gah.Blocks.DomainBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The abstract Domain Command Handler Base class.
    /// </summary>
    /// <typeparam name="TCommand">The command to handle.</typeparam>
    public abstract class DomainCommandHandler<TCommand> : IDomainCommandHandler<TCommand>
        where TCommand : IDomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommandHandler{TCommand}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public DomainCommandHandler(ILogger logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }

        /// <inheritdoc/>
        async Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public abstract Task Handle(TCommand command, CancellationToken cancellationToken = default);
    }
}
