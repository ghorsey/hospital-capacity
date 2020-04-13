namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindHospitalViewByIdQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{HospitalView}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{HospitalView}" />
    public class FindHospitalViewByIdQuery : DomainQuery<HospitalView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalViewByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public FindHospitalViewByIdQuery(Guid id, string correlationId)
            : base(correlationId)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }
    }
}
