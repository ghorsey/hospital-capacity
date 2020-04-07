namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class RapidHospitalUpdateCommandHandler : DomainCommandHandlerBase<RapidHospitalUpdateCommand>
    {
        private readonly IHospitalCapacityUow uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="RapidHospitalUpdateCommandHandler"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="logger">The logger.</param>
        public RapidHospitalUpdateCommandHandler(IHospitalCapacityUow uow, ILogger<RapidHospitalUpdateCommandHandler> logger)
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
        /// <exception cref="ArgumentNullException">command.</exception>
        public override async Task Handle(RapidHospitalUpdateCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation("Calling rapid update"); // todo: put better log mesasage

            var hospital = await this.uow.HospitalRepository.FindAsync(command.Id, cancellationToken);
            var capacity = new HospitalCapacity
            {
                BedCapacity = command.BedCapacity,
                BedsInUse = command.BedsInUse,
                HospitalId = command.Id,
            };

            capacity.CalculatePercentOfUsage();

            await this.uow.ExecuteInResilientTransactionAsync(async () =>
            {
                hospital.BedsInUse = capacity.BedsInUse;
                hospital.BedCapacity = capacity.BedCapacity;
                hospital.PercentOfUsage = capacity.PercentOfUsage;
                hospital.IsCovid = command.IsCovid;

                cancellationToken.ThrowIfCancellationRequested();

                await this.uow.HospitalCapacityRepository.AddAsync(capacity).ConfigureAwait(false);
                await this.uow.HospitalRepository.UpdateAsync(hospital).ConfigureAwait(false);

                await this.uow.CommitAsync().ConfigureAwait(false);

                return true;
            }).ConfigureAwait(false);
        }
    }
}
