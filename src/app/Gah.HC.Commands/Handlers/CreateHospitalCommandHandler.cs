namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class CreateHospitalCommandHandler.
    /// Implements the <see cref="Gah.Blocks.EventBus.DomainCommandHandlerBase{CreateHospitalCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.DomainCommandHandlerBase{CreateHospitalCommand}" />
    public class CreateHospitalCommandHandler : DomainCommandHandlerBase<CreateHospitalCommand>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHospitalCommandHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">uow.</exception>
        public CreateHospitalCommandHandler(IHospitalCapacityUow uow, ILogger<CreateHospitalCommandHandler> logger)
            : base(logger)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
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
                BedCapacity = command.Hospital.BedCapacity,
                BedsInUse = command.Hospital.BedsInUse,
            };

            capacity.CalculatePercentageAvailable();

            command.Hospital.PercentageAvailable = capacity.PercentageAvailable;

            await this.uow.HospitalCapacityRepository.AddAsync(capacity).ConfigureAwait(false);
            await this.uow.HospitalRepository.AddAsync(command.Hospital).ConfigureAwait(false);
            await this.uow.CommitAsync().ConfigureAwait(false);
        }
    }
}
