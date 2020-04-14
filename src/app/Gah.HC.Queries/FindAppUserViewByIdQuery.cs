namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindAppUserViewByIdQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{AppUserView}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{AppUserView}" />
    public class FindAppUserViewByIdQuery : DomainQuery<AppUserView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindAppUserViewByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public FindAppUserViewByIdQuery(Guid id, string correlationId)
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
