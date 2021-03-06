﻿namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Collections.Generic;
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
    /// Class RegisterRegionUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandler{RegisterRegionUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainCommandHandler{RegisterRegionUserCommand}" />
    public class RegisterRegionUserCommandHandler : DomainCommandHandler<RegisterRegionUserCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly UserManager<AppUser> userManager;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRegionUserCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// domainBus
        /// or
        /// uow
        /// or
        /// userManager.
        /// </exception>
        public RegisterRegionUserCommandHandler(
            IHospitalCapacityUow uow,
            UserManager<AppUser> userManager,
            IDomainBus domainBus,
            ILogger<RegisterRegionUserCommandHandler> logger)
            : base(logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
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
                var region = await this.uow.RegionRepository.FindByNameAsync(command.RegionName, cancellationToken).ConfigureAwait(false);
                var isApproved = false;

                if (region == null)
                {
                    region = new Region
                    {
                        Name = command.RegionName,
                    };

                    isApproved = true;

                    await this.uow.RegionRepository.AddAsync(region).ConfigureAwait(false);
                    await this.uow.CommitAsync().ConfigureAwait(false);
                }

                var user = new AppUser
                {
                    UserName = command.Email,
                    UserType = AppUserType.Region,
                    RegionId = region.Id,
                    IsApproved = isApproved,
                };

                var result = await this.userManager.CreateAsync(user, command.Password).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    throw new UserCreationException("Failed to create user", result.Errors ?? new List<IdentityError>());
                }

                var appUserUpdatedEvent = new AppUserUpdatedEvent(user, command.CorrelationId);

                await this.domainBus.PublishAsync(cancellationToken, appUserUpdatedEvent).ConfigureAwait(false);

                return true;
            }).ConfigureAwait(false);
        }
    }
}
