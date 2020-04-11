namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class SetUserAuthorizedCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandler{SetUserAuthorizedCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandler{SetUserAuthorizedCommand}" />
    public class SetUserAuthorizedCommandHandler : DomainCommandHandler<SetUserAuthorizedCommand>
    {
        private readonly IAppUserRepository appUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserAuthorizedCommandHandler"/> class.
        /// </summary>
        /// <param name="userStore">The user store.</param>
        /// <param name="logger">The logger.</param>
        public SetUserAuthorizedCommandHandler(IUserStore<AppUser> userStore, ILogger<SetUserAuthorizedCommandHandler> logger)
            : base(logger)
        {
            this.appUserRepository = (IAppUserRepository)userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">command.</exception>
        public override async Task Handle(SetUserAuthorizedCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            this.Logger.LogInformation($"Setting user is authorized for {command.User.Id} to {command.IsAuthorized}");

            await this.appUserRepository.SetUserAuthorized(command.User, command.IsAuthorized, cancellationToken).ConfigureAwait(false);
        }
    }
}
