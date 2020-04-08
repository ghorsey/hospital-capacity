namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Spa.Models.Hospitals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AddHospitalRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{AddHospitalRequirement, CreateHospitalInput}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{AddHospitalRequirement, CreateHospitalInput}" />
    public class CreateHospitalRequirementHandler : AuthorizationHandler<CreateHospitalRequirement, CreateHospitalInput>
    {
        private ILogger<CreateHospitalRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHospitalRequirementHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public CreateHospitalRequirementHandler(ILogger<CreateHospitalRequirementHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement and resource.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <param name="resource">The resource to evaluate.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateHospitalRequirement requirement, CreateHospitalInput resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));
            this.logger.LogInformation("Handling requirement AddEditHospitalRequirement");

            var role = context.User.FindFirst(ClaimTypes.Role);

            if (role.Value.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            if (role.Value.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value.EndsWith(resource.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
