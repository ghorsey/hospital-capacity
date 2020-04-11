namespace Gah.HC.Queries
{
    using System;
    using System.Security.Claims;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindUserByClaimsPrincipal.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{AppUser}" />
    public class FindUserByClaimsPrincipalQuery : DomainQuery<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByClaimsPrincipalQuery"/> class.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <exception cref="ArgumentNullException">claimsPrincipal.</exception>
        public FindUserByClaimsPrincipalQuery(ClaimsPrincipal claimsPrincipal, string correlationId)
            : base(correlationId)
        {
            this.ClaimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        /// <summary>
        /// Gets the claims principal.
        /// </summary>
        /// <value>The claims principal.</value>
        public ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
