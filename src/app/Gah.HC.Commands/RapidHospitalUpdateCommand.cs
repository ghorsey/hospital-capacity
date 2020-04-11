namespace Gah.HC.Commands
{
    using System;
    using Gah.Blocks.DomainBus;

    /// <summary>
    /// Class RapidHospitalUpdateCommand.
    /// </summary>
    public class RapidHospitalUpdateCommand : DomainCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RapidHospitalUpdateCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bedsInUse">The beds in use.</param>
        /// <param name="bedCapacity">The bed capacity.</param>
        /// <param name="isCovid">if set to <c>true</c> [is covid].</param>
        public RapidHospitalUpdateCommand(Guid id, int bedsInUse, int bedCapacity, bool isCovid)
        {
            this.Id = id;
            this.BedCapacity = bedCapacity;
            this.BedsInUse = bedsInUse;
            this.IsCovid = isCovid;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the beds in use.
        /// </summary>
        /// <value>The beds in use.</value>
        public int BedsInUse { get; }

        /// <summary>
        /// Gets the bed capacity.
        /// </summary>
        /// <value>The bed capacity.</value>
        public int BedCapacity { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is covid.
        /// </summary>
        /// <value><c>true</c> if this instance is covid; otherwise, <c>false</c>.</value>
        public bool IsCovid { get; }
    }
}
