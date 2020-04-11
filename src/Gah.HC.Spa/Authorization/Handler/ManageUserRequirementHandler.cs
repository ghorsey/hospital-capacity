namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class ManagerUserRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ManagerUserRequirement, AppUser}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ManagerUserRequirement, AppUser}" />
    public class ManageUserRequirementHandler : AuthorizationHandler<ManageUserRequirement, AppUser>
    {
        private readonly ILogger<ManageUserRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUserRequirementHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public ManageUserRequirementHandler(ILogger<ManageUserRequirementHandler> logger)
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
        /// <exception cref="ArgumentNullException">
        /// context
        /// or
        /// resource.
        /// </exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageUserRequirement requirement, AppUser resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));

            this.logger.LogInformation("Attempting to manage a user object");

            var role = context.User.FindFirst(ClaimTypes.Role).Value;

            if (role.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is an admin user");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var regionId = context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value;
            if (role.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                regionId.Equals(resource.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is a region user of the specified user");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId.Equals(resource.Id.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("A user is managing their own record");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            this.logger.LogInformation("Access denied");
            return Task.CompletedTask;
        }
    }
}
