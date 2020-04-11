namespace Gah.HC.Queries
{
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindUserByEmailQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />
    public class FindUserByEmailQuery : DomainQuery<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByEmailQuery"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public FindUserByEmailQuery(string email, string correlationId)
            : base(correlationId)
        {
            this.Email = email;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; }
    }
}
