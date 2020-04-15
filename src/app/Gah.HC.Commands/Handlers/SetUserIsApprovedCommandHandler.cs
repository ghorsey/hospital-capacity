namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class SetUserAuthorizedCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandler{SetUserAuthorizedCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandler{SetUserAuthorizedCommand}" />
    public class SetUserIsApprovedCommandHandler : DomainCommandHandler<SetUserIsApprovedCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly IUserStore<AppUser> appUserRepository;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserIsApprovedCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="userStore">The user store.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// domainBus
        /// or
        /// userStore
        /// or
        /// uow.
        /// </exception>
        public SetUserIsApprovedCommandHandler(IHospitalCapacityUow uow, IDomainBus domainBus, IUserStore<AppUser> userStore, ILogger<SetUserIsApprovedCommandHandler> logger)
            : base(logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.appUserRepository = userStore ?? throw new ArgumentNullException(nameof(userStore));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">command.</exception>
        public override async Task Handle(SetUserIsApprovedCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            this.Logger.LogInformation($"Setting user is authorized for {command.User.Id} to {command.IsApproved}");

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                await ((IAppUserRepository)this.appUserRepository).SetUserIsApprovedAsync(command.User, command.IsApproved, cancellationToken).ConfigureAwait(false);

                var user = await this.appUserRepository.FindByIdAsync(command.User.Id, cancellationToken).ConfigureAwait(false);

                var e = new AppUserUpdatedEvent(user, command.CorrelationId);

                await this.domainBus.PublishAsync(cancellationToken, e).ConfigureAwait(false);
                return true;
            }).ConfigureAwait(false);
        }
    }
}
