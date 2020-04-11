namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class SetUserApprovedRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{SetUserApprovedRequirement, AppUser}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{SetUserApprovedRequirement, AppUser}" />
    public class SetUserApprovedRequirementHandler : AuthorizationHandler<SetUserApprovedRequirement, AppUser>
    {
        private readonly ILogger<SetUserApprovedRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserApprovedRequirementHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public SetUserApprovedRequirementHandler(ILogger<SetUserApprovedRequirementHandler> logger)
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
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SetUserApprovedRequirement requirement, AppUser resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(context));
            this.logger.LogInformation("Attempting to set approval status on a user");

            var role = context.User.FindFirst(ClaimTypes.Role).Value;

            if (role.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The logged in user is an admin, succeeding");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var regionId = context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value;
            if (role.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                regionId.Equals(resource.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The logged in use is a region manager for the target user's region");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            this.logger.LogInformation("Access denied");
            return Task.CompletedTask;
        }
    }
}
