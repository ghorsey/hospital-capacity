namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class RapidHospitalUpdateCommandHandler : DomainCommandHandlerBase<RapidHospitalUpdateCommand>
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
            hospital.BedCapacity = command.BedCapacity;
            hospital.BedsInUse = command.BedsInUse;
            hospital.IsCovid = command.IsCovid;

            await this.domainBus.ExecuteAsync(new UpdateHospitalCommand(hospital, command.CorrelationId), cancellationToken).ConfigureAwait(false);
        }
    }
}
