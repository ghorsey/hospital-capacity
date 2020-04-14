namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands.Exceptions;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegisterHospitalUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandler{RegisterHospitalUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandler{RegisterHospitalUserCommand}" />
    public class RegisterHospitalUserCommandHandler : DomainCommandHandler<RegisterHospitalUserCommand>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IHospitalCapacityUow uow;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHospitalUserCommandHandler" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// domainBus
        /// or
        /// userManager.
        /// </exception>
        public RegisterHospitalUserCommandHandler(
            UserManager<AppUser> userManager,
            IHospitalCapacityUow uow,
            IDomainBus domainBus,
            ILogger<RegisterHospitalUserCommandHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">command.</exception>
        /// <exception cref="UserCreationException">Failed to create user.</exception>
        public override async Task Handle(RegisterHospitalUserCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation($"Regisering hospital user {command.Email} for hospital {command.HospitalId}");

            var user = new AppUser
            {
                UserName = command.Email,
                UserType = AppUserType.Hospital,
                RegionId = command.RegionId,
                HospitalId = command.HospitalId,
            };

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                var result = await this.userManager.CreateAsync(user, command.Password).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    throw new UserCreationException("Failed to create user", result.Errors);
                }

                var appUserUpdatedEvent = new AppUserUpdatedEvent(user, command.CorrelationId);

                await this.domainBus.PublishAsync(cancellationToken, appUserUpdatedEvent).ConfigureAwait(false);

                return true;
            }).ConfigureAwait(false);
        }
    }
}
