namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands.Exceptions;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegisterRegionUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{RegisterRegionUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainCommandHandler{RegisterRegionUserCommand}" />
    public class RegisterRegionUserCommandHandler : DomainCommandHandlerBase<RegisterRegionUserCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRegionUserCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public RegisterRegionUserCommandHandler(
            IHospitalCapacityUow uow,
            UserManager<AppUser> userManager,
            ILogger<RegisterRegionUserCommandHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc/>
        public override async Task Handle(RegisterRegionUserCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation($"Attempting to register a user for the region {command.RegionName}");

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                var region = new Region
                {
                    Name = command.RegionName,
                };

                await this.uow.RegionRepository.AddAsync(region).ConfigureAwait(false);
                await this.uow.CommitAsync().ConfigureAwait(false);

                var user = new AppUser
                {
                    UserName = command.Email,
                    UserType = AppUserType.Region,
                    RegionId = region.Id,
                };

                var result = await this.userManager.CreateAsync(user, command.Password).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    throw new UserCreationException("Failed to create user", result.Errors ?? new List<IdentityError>());
                }

                return true;
            }).ConfigureAwait(false);
        }
    }
}
