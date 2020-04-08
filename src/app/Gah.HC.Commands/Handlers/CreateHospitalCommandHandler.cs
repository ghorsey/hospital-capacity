namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class CreateHospitalCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{CreateHospitalCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{CreateHospitalCommand}" />
    public class CreateHospitalCommandHandler : DomainCommandHandlerBase<CreateHospitalCommand>
    {
        private readonly IHospitalCapacityUow uow;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHospitalCommandHandler" /> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public CreateHospitalCommandHandler(IHospitalCapacityUow uow, IDomainBus domainBus, ILogger<CreateHospitalCommandHandler> logger)
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
        public override async Task Handle(CreateHospitalCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation($"Attempting to create new hospital named: {command.Hospital.Name} for region id: {command.Hospital.RegionId}");

            var capacity = new HospitalCapacity
            {
                HospitalId = command.Hospital.Id,
                BedCapacity = command.Hospital.BedCapacity,
                BedsInUse = command.Hospital.BedsInUse,
            };

            capacity.CalculatePercentOfUsage();

            command.Hospital.PercentOfUsage = capacity.PercentOfUsage;

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                await this.uow.HospitalRepository.AddAsync(command.Hospital).ConfigureAwait(false);
                await this.uow.HospitalCapacityRepository.AddAsync(capacity).ConfigureAwait(false);
                await this.uow.CommitAsync().ConfigureAwait(false);

                var region = await this.uow.RegionRepository.FindAsync(command.Hospital.RegionId).ConfigureAwait(false);
                var hospitalChangedEvent = new HospitalChangedEvent(command.Hospital, region, new List<HospitalCapacity> { capacity }, command.CorrelationId);

                await this.domainBus.PublishAsync(cancellationToken, hospitalChangedEvent).ConfigureAwait(false);

                return true;
            }).ConfigureAwait(false);
        }
    }
}
