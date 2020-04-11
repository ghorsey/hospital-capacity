namespace Gah.HC.Queries
{
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindUserByIdQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />
    public class FindUserByIdQuery : DomainQuery<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByIdQuery" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public FindUserByIdQuery(string userId, string correlationId)
            : base(correlationId)
        {
            this.UserId = userId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; }
    }
}
