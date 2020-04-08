namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Spa.Models.Hospitals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class UpdateHospitalRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{UpdateHospitalRequirement, UpdateHospitalInput}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{UpdateHospitalRequirement, UpdateHospitalInput}" />
    public class UpdateHospitalRequirementHandler : AuthorizationHandler<UpdateHospitalRequirement, Hospital>
    {
        private readonly ILogger<UpdateHospitalRequirementHandler> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateHospitalRequirementHandler"/> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// domainBus
        /// or
        /// httpContextAccessor
        /// or
        /// logger.
        /// </exception>
        public UpdateHospitalRequirementHandler(
            IDomainBus domainBus,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UpdateHospitalRequirementHandler> logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// handle requirement as an asynchronous operation.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <param name="resource">The resource to evaluate.</param>
        /// <returns>Task.</returns>
        /// <exception cref="ArgumentNullException">context
        /// or
        /// resource.</exception>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UpdateHospitalRequirement requirement,
            Hospital resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));
            this.logger.LogInformation($"Checking if user has access to update hospital: {resource.Id}");

            var role = context.User.FindFirst(ClaimTypes.Role);

            if (role.Value.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access grandted to super user");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (role.Value.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value.EndsWith(resource.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access granted to region user");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (role.Value.Equals(AppUserType.Hospital.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.HospitalClaimType).Value.EndsWith(resource.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access granted to hospital user");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask; // failed checks.
        }
    }
}
