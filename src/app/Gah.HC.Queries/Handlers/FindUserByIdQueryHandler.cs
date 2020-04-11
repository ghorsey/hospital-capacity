namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class FindUserByIdQueryHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindUserByIdQuery, AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandler{FindUserByIdQuery, AppUser}" />
    public class FindUserByIdQueryHandler : DomainQueryHandler<FindUserByIdQuery, AppUser>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userManager.</exception>
        public FindUserByIdQueryHandler(UserManager<AppUser> userManager, ILogger<FindUserByIdQueryHandler> logger)
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
        public override Task<AppUser> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            this.Logger.LogInformation($"Attempting to find user '{request.UserId}'");

            return this.userManager.FindByIdAsync(request.UserId);
        }
    }
}
