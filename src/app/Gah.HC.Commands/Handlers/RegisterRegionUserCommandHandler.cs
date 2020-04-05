namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegisterRegionUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.EventBus.DomainCommandHandlerBase{RegisterRegionUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.IDomainCommandHandler{RegisterRegionUserCommand}" />
    public class RegisterRegionUserCommandHandler : DomainCommandHandlerBase<RegisterRegionUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRegionUserCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public RegisterRegionUserCommandHandler(ILogger<RegisterRegionUserCommandHandler> logger)
            : base(logger)
        {
        }

        /// <inheritdoc/>
        protected override Task Handle(RegisterRegionUserCommand command, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
