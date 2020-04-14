namespace Gah.HC.Events.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AppUserUpdatedEventHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainEventHandler{AppUserUpdatedEvent}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainEventHandler{AppUserUpdatedEvent}" />
    public class AppUserUpdatedEventHandler : IDomainEventHandler<AppUserUpdatedEvent>
    {
        private readonly ILogger<AppUserUpdatedEventHandler> logger;
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserUpdatedEventHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// logger.
        /// </exception>
        public AppUserUpdatedEventHandler(IHospitalCapacityUow uow, ILogger<AppUserUpdatedEventHandler> logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task Handle(AppUserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            notification = notification ?? throw new ArgumentNullException(nameof(notification));

            this.logger.LogInformation($"User {notification.User.Id} was updated");

            var add = false;
            Region? region = null;
            Hospital? hospital = null;
            var user = await this.uow.AppUserViewRepository
                .FindAsync(
                    Guid.Parse(notification.User.Id),
                    cancellationToken)
                .ConfigureAwait(false);

            if (user == null)
            {
                user = new AppUserView(Guid.Parse(notification.User.Id));
                add = true;
            }

            if (notification.User.RegionId.HasValue)
            {
                region = await this.uow.RegionRepository.FindAsync(notification.User.RegionId.Value).ConfigureAwait(false);
            }

            if (notification.User.HospitalId.HasValue)
            {
                hospital = await this.uow.HospitalRepository.FindAsync(notification.User.HospitalId.Value).ConfigureAwait(false);
            }

            user.HospitalId = notification.User.HospitalId;
            user.HospitalName = hospital?.Name;
            user.IsApproved = notification.User.IsApproved;
            user.RegionId = notification.User.RegionId;
            user.RegionName = region?.Name;
            user.UserName = notification.User.UserName;
            user.UserType = notification.User.UserType;

            if (add)
            {
                await this.uow.AppUserViewRepository.AddAsync(user).ConfigureAwait(false);
            }
            else
            {
                await this.uow.AppUserViewRepository.UpdateAsync(user).ConfigureAwait(false);
            }

            await this.uow.CommitAsync().ConfigureAwait(false);
        }
    }
}
