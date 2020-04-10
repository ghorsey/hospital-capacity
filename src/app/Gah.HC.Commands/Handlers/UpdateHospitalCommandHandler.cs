namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Repository;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class UpdateHospitalCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{UpdateHospitalCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{UpdateHospitalCommand}" />
    public class UpdateHospitalCommandHandler : DomainCommandHandlerBase<UpdateHospitalCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateHospitalCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public UpdateHospitalCommandHandler(IHospitalCapacityUow uow, IDomainBus domainBus, ILogger<UpdateHospitalCommandHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">command.</exception>
        public override async Task Handle(UpdateHospitalCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            this.Logger.LogInformation($"Updating hospital {command.Hospital.Id}");

            var current = await this.uow.HospitalRepository.FindAsync(command.Hospital.Id).ConfigureAwait(false);

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                if (current.BedCapacity != command.Hospital.BedCapacity || current.BedsInUse != command.Hospital.BedsInUse)
                {
                    var capacity = new HospitalCapacity()
                    {
                        BedCapacity = command.Hospital.BedCapacity,
                        BedsInUse = command.Hospital.BedsInUse,
                        HospitalId = command.Hospital.Id,
                    };

                    capacity.CalculatePercentOfUsage();

                    command.Hospital.PercentOfUsage = capacity.PercentOfUsage;
                    command.Hospital.UpdatedOn = DateTime.UtcNow;

                    await this.uow.HospitalCapacityRepository.AddAsync(capacity).ConfigureAwait(false);
                }

                cancellationToken.ThrowIfCancellationRequested();
                await this.uow.HospitalRepository.UpdateAsync(command.Hospital).ConfigureAwait(false);
                await this.uow.CommitAsync().ConfigureAwait(false);

                await PublishHospitalChanedEvent().ConfigureAwait(false);
                return true;

                async Task PublishHospitalChanedEvent()
                {
                    var region = await this.uow.RegionRepository.FindAsync(command.Hospital.RegionId, cancellationToken).ConfigureAwait(false);
                    var recentCapacity = await this.uow.HospitalCapacityRepository.GetRecentAsync(command.Hospital.Id, 10).ConfigureAwait(false);

                    var hospitalChanged = new HospitalChangedEvent(command.Hospital, region, recentCapacity, command.CorrelationId);
                    await this.domainBus.PublishAsync(cancellationToken, hospitalChanged).ConfigureAwait(false);
                }
            }).ConfigureAwait(false);
        }
    }
}
