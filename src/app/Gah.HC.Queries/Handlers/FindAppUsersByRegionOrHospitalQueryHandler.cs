namespace Gah.HC.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class FindAppUsersByRegionOrHospitalQueryHandler : DomainQueryHandler<FindAppUsersByRegionOrHospitalQuery, List<AppUser>>
    {
        private IAppUserRepository appUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAppUsersByRegionOrHospitalQueryHandler"/> class.
        /// </summary>
        /// <param name="userStore">The user store.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userStore.</exception>
        public FindAppUsersByRegionOrHospitalQueryHandler(IUserStore<AppUser> userStore, ILogger<FindAppUsersByRegionOrHospitalQueryHandler> logger)
            : base(logger)
        {
            this.appUserRepository = (IAppUserRepository)userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;AppUser&gt;&gt;.</returns>
        /// <inheritdoc />
        public override Task<List<AppUser>> Handle(FindAppUsersByRegionOrHospitalQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            this.Logger.LogInformation($"Finding users of either region '{request.RegionId}' or hospital '{request.HospitalId}'");
            return this.appUserRepository.FindByAsync(request.RegionId, request.HospitalId, cancellationToken);
        }
    }
}
