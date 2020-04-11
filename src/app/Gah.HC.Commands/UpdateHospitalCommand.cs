namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class UpdateHospitalCommand.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommand" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommand" />
    public class UpdateHospitalCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateHospitalCommand"/> class.
        /// </summary>
        /// <param name="hospital">The hospital.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">hospital.</exception>
        public UpdateHospitalCommand(Hospital hospital, string correlationId)
            : base(correlationId)
        {
            this.Hospital = hospital ?? throw new ArgumentNullException(nameof(hospital));
        }

        /// <summary>
        /// Gets the hospital.
        /// </summary>
        /// <value>The hospital.</value>
        public Hospital Hospital { get; }
    }
}
