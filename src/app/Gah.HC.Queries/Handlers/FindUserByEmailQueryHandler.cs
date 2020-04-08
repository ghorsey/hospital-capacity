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
    /// Class FindUserByEmailQueryHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindUserByEmailQuery, AppUser}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryHandlerBase{FindUserByEmailQuery, AppUser}" />
    public class FindUserByEmailQueryHandler : DomainQueryHandlerBase<FindUserByEmailQuery, AppUser>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserByEmailQueryHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userManager.</exception>
        public FindUserByEmailQueryHandler(UserManager<AppUser> userManager, ILogger logger)
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
        /// <inheritdoc />
        public override Task<AppUser> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding user by email");

            request = request ?? throw new ArgumentNullException(nameof(request));

            return this.userManager.FindByNameAsync(request.Email);
        }
    }
}
