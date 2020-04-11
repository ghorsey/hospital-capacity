namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class ViewRegionUsersRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ViewRegionUsersRequirement}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ViewRegionUsersRequirement}" />
    public class ViewRegionUsersRequirementHandler : AuthorizationHandler<ViewRegionUsersRequirement, Region>
    {
        private readonly ILogger<ViewRegionUsersRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRegionUsersRequirementHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public ViewRegionUsersRequirementHandler(
            ILogger<ViewRegionUsersRequirementHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement and resource.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <param name="resource">The resource to evaluate.</param>
        /// <returns>Task.</returns>
        /// <exception cref="ArgumentNullException">context.</exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRegionUsersRequirement requirement,  Region resource)
        {
            resource = resource ?? throw new ArgumentNullException(nameof(resource));
            context = context ?? throw new ArgumentNullException(nameof(context));

            this.logger.LogInformation("Testing if the user has access to the specified region");

            var role = context.User.FindFirst(ClaimTypes.Role).Value;

            if (role.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is an admin");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var regionId = context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value;
            if (role.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                regionId.Equals(resource.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
