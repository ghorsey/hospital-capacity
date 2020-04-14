namespace Gah.HC.Events.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalChangedUpdateUserViewHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainEventHandler{ChangedEvent}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainEventHandler{ChangedEvent}" />
    public class HospitalChangedUpdateUserViewHandler : IDomainEventHandler<HospitalChangedEvent>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly ILogger<HospitalChangedUpdateUserViewHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalChangedUpdateUserViewHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// logger.
        /// </exception>
        public HospitalChangedUpdateUserViewHandler(IHospitalCapacityUow uow, ILogger<HospitalChangedUpdateUserViewHandler> logger)
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
        /// <exception cref="ArgumentNullException">notification.</exception>
        public async Task Handle(HospitalChangedEvent notification, CancellationToken cancellationToken)
        {
            notification = notification ?? throw new ArgumentNullException(nameof(notification));
            this.logger.LogInformation($"Updating names for the hospital {notification.Hospital.Id}");

            var users = await this.uow.AppUserViewRepository
                .FindByAsync(
                    hospitalId: notification.Hospital.Id,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            foreach (var user in users)
            {
                user.HospitalName = notification.Hospital.Name;
            }

            await this.uow.AppUserViewRepository.UpdateRangeAsync(users).ConfigureAwait(false);
            await this.uow.CommitAsync().ConfigureAwait(false);
        }
    }
}
