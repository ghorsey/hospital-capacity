namespace Gah.HC.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class CreateHospitalCommand.
    /// Implements the <see cref="Gah.Blocks.EventBus.DomainCommandBase" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.DomainCommandBase" />
    public class CreateHospitalCommand : DomainCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHospitalCommand" /> class.
        /// </summary>
        /// <param name="hospital">The hospital.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">hospital.</exception>
        public CreateHospitalCommand(Hospital hospital, string correlationId)
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
