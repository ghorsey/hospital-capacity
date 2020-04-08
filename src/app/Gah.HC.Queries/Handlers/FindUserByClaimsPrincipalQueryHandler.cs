namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class FindUserByClaimsPrincipalQueryHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindUserByClaimsPrincipalQuery, AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindUserByClaimsPrincipalQuery, AppUser}" />
    public class FindUserByClaimsPrincipalQueryHandler : DomainQueryHandlerBase<FindUserByClaimsPrincipalQuery, AppUser>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByClaimsPrincipalQueryHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public FindUserByClaimsPrincipalQueryHandler(UserManager<AppUser> userManager, ILogger logger)
            : base(logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppUser&gt;.</returns>
        /// <exception cref="ArgumentNullException">request.</exception>
        /// <inheritdoc />
        public override Task<AppUser> Handle(FindUserByClaimsPrincipalQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            this.Logger.LogInformation("Finding a user by claims principal");

            cancellationToken.ThrowIfCancellationRequested();

            return this.userManager.GetUserAsync(request.ClaimsPrincipal);
        }
    }
}
