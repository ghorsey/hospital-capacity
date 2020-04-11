namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Models.Hospitals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class EditHospitalRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{EditHospitalRequirement, RapidHospitalUpdateInput}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{RapidHospitalUpdateRequirement, RapidHospitalUpdateInput}" />
    public class RapidHospitalUpdateRequirementHandler : AuthorizationHandler<RapidHospitalUpdateRequirement, RapidHospitalUpdateInput>
    {
        private readonly ILogger<RapidHospitalUpdateRequirementHandler> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RapidHospitalUpdateRequirementHandler" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public RapidHospitalUpdateRequirementHandler(
            IHttpContextAccessor httpContextAccessor,
            IDomainBus domainBus,
            ILogger<RapidHospitalUpdateRequirementHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement and resource.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <param name="resource">The resource to evaluate.</param>
        /// <returns>Task.</returns>
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RapidHospitalUpdateRequirement requirement,
            RapidHospitalUpdateInput resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));
            this.logger.LogInformation($"Performing rapid update for hospital {resource.Id}");

            var role = context.User.FindFirst(ClaimTypes.Role);

            if (role.Value.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access grandted to super user");
                context.Succeed(requirement);
                return;
            }

            var hospital = await this.domainBus.ExecuteAsync(
                new FindHospitalBySlugOrIdQuery(
                    this.httpContextAccessor.HttpContext.TraceIdentifier,
                    id: resource.Id));

            if (role.Value.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value.EndsWith(hospital.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access granted to region user");
                context.Succeed(requirement);
                return;
            }

            if (role.Value.Equals(AppUserType.Hospital.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.HospitalClaimType).Value.EndsWith(hospital.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("Access granted to hospital user");
                context.Succeed(requirement);
                return;
            }
        }
    }
}
