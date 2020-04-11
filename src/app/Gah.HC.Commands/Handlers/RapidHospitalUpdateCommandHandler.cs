namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class RapidHospitalUpdateCommandHandler : DomainCommandHandler<RapidHospitalUpdateCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RapidHospitalUpdateCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// domainBus.
        /// </exception>
        public RapidHospitalUpdateCommandHandler(IHospitalCapacityUow uow, IDomainBus domainBus, ILogger<RapidHospitalUpdateCommandHandler> logger)
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
        public override async Task Handle(RapidHospitalUpdateCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation("Calling rapid update"); // todo: put better log mesasage

            var hospital = await this.uow.HospitalRepository.FindAsync(command.Id, cancellationToken);

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                if (hospital.BedCapacity != command.BedCapacity || hospital.BedsInUse != command.BedsInUse)
                {
                    var capacity = new HospitalCapacity
                    {
                        BedCapacity = command.BedCapacity,
                        BedsInUse = command.BedsInUse,
                        HospitalId = command.Id,
                    };

                    capacity.CalculatePercentOfUsage();
                    hospital.BedsInUse = capacity.BedsInUse;
                    hospital.BedCapacity = capacity.BedCapacity;
                    hospital.PercentOfUsage = capacity.PercentOfUsage;
                    hospital.UpdatedOn = DateTime.UtcNow;

                    await this.uow.HospitalCapacityRepository.AddAsync(capacity).ConfigureAwait(false);
                }

                hospital.IsCovid = command.IsCovid;

                cancellationToken.ThrowIfCancellationRequested();

                await this.uow.HospitalRepository.UpdateAsync(hospital).ConfigureAwait(false);

                await this.uow.CommitAsync().ConfigureAwait(false);

                await PublishHospitalChanedEvent().ConfigureAwait(false);

                return true;

                async Task PublishHospitalChanedEvent()
                {
                    var region = await this.uow.RegionRepository.FindAsync(hospital.RegionId, cancellationToken).ConfigureAwait(false);
                    var recentCapacity = await this.uow.HospitalCapacityRepository.GetRecentAsync(hospital.Id, 10).ConfigureAwait(false);

                    var hospitalChanged = new HospitalChangedEvent(hospital, region, recentCapacity, command.CorrelationId);
                    await this.domainBus.PublishAsync(cancellationToken, hospitalChanged).ConfigureAwait(false);
                }
            }).ConfigureAwait(false);
        }
    }
}
