namespace Gah.HC.Events.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalChangedEventHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.IDomainEventHandler{HospitalChangedEvent}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.IDomainEventHandler{HospitalChangedEvent}" />
    public class HospitalChangedEventHandler : IDomainEventHandler<HospitalChangedEvent>
    {
        private IHospitalCapacityUow uow;
        private ILogger<HospitalChangedEventHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalChangedEventHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// logger.
        /// </exception>
        public HospitalChangedEventHandler(IHospitalCapacityUow uow, ILogger<HospitalChangedEventHandler> logger)
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
        public async Task Handle(HospitalChangedEvent notification, CancellationToken cancellationToken)
        {
            notification = notification ?? throw new ArgumentNullException(nameof(notification));

            this.logger.LogInformation("Updaing hospital view");

            var view = new HospitalView(notification.Hospital.Id)
            {
                Address1 = notification.Hospital.Address1,
                Address2 = notification.Hospital.Address2,
                BedCapacity = notification.Hospital.BedCapacity,
                BedsInUse = notification.Hospital.BedsInUse,
                City = notification.Hospital.City,
                CreatedOn = notification.Hospital.CreatedOn,
                IsCovid = notification.Hospital.IsCovid,
                Name = notification.Hospital.Name,
                PercentOfUsage = notification.Hospital.PercentOfUsage,
                Phone = notification.Hospital.Phone,
                PostalCode = notification.Hospital.PostalCode,
                RegionId = notification.Hospital.RegionId,
                RegionName = notification.Region.Name,
                Slug = notification.Hospital.Slug,
                State = notification.Hospital.State,
                UpdatedOn = notification.Hospital.UpdatedOn,
            };

            view.Capacity1 = GetCapacity(1);
            view.Capacity2 = GetCapacity(2);
            view.Capacity3 = GetCapacity(3);
            view.Capacity4 = GetCapacity(4);
            view.Capacity5 = GetCapacity(5);
            view.Capacity6 = GetCapacity(6);
            view.Capacity7 = GetCapacity(7);
            view.Capacity8 = GetCapacity(8);
            view.Capacity9 = GetCapacity(9);
            view.Capacity10 = GetCapacity(10);

            view.Used1 = GetUsed(1);
            view.Used2 = GetUsed(2);
            view.Used3 = GetUsed(3);
            view.Used4 = GetUsed(4);
            view.Used5 = GetUsed(5);
            view.Used6 = GetUsed(6);
            view.Used7 = GetUsed(7);
            view.Used8 = GetUsed(8);
            view.Used9 = GetUsed(9);
            view.Used10 = GetUsed(10);

            cancellationToken.ThrowIfCancellationRequested();

            await this.uow.HospitalViewRepository.AddAsync(view).ConfigureAwait(false);
            await this.uow.CommitAsync().ConfigureAwait(false);

            int GetCapacity(int number)
            {
                if (number > notification.Capacity.Count)
                {
                    return 0;
                }

                return notification.Capacity[number - 1]?.BedCapacity ?? 0;
            }

            int GetUsed(int number)
            {
                if (number > notification.Capacity.Count)
                {
                    return 0;
                }

                return notification.Capacity[number - 1]?.BedsInUse ?? 0;
            }
        }
    }
}
