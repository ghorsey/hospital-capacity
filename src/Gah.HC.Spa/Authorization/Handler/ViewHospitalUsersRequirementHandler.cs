namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class ViewHospitalUsersRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ViewHospitalUsersRequirement, Hospital}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{ViewHospitalUsersRequirement, Hospital}" />
    public class ViewHospitalUsersRequirementHandler : AuthorizationHandler<ViewHospitalUsersRequirement, Hospital>
    {
        private readonly ILogger<ViewHospitalUsersRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewHospitalUsersRequirementHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public ViewHospitalUsersRequirementHandler(ILogger<ViewHospitalUsersRequirementHandler> logger)
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
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewHospitalUsersRequirement requirement, Hospital resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));

            this.logger.LogInformation("Attemptint to check if the user has acces to view hospital users");

            var role = context.User.FindFirst(ClaimTypes.Role).Value;

            if (role.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Current user is an admin");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var regionId = context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value;
            if (role.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                regionId.Equals(resource.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is a region user for the specified hospital");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var hospitalId = context.User.FindFirst(ClaimsPrincipalFactory.HospitalClaimType).Value;
            if (role.Equals(AppUserType.Hospital.ToString(), StringComparison.OrdinalIgnoreCase) &&
                hospitalId.Equals(resource.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is a hospital user of the specified hospital");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            this.logger.LogInformation("Access denied");
            return Task.CompletedTask;
        }
    }
}
