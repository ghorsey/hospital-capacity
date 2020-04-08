namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Repository;
    using Gah.HC.Spa.Models.Authorization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegisterHospitalUserRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{RegisterHospitalUserRequirement, RegisterHospitalUserInput}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{RegisterHospitalUserRequirement, RegisterHospitalUserInput}" />
    public class RegisterHospitalUserRequirementHandler : AuthorizationHandler<RegisterHospitalUserRequirement, RegisterHospitalUserInput>
    {
        private readonly IDomainBus domainBus;
        private readonly ILogger<RegisterHospitalUserRequirementHandler> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHospitalUserRequirementHandler" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">domainBus
        /// or
        /// logger.</exception>
        public RegisterHospitalUserRequirementHandler(
            IDomainBus domainBus,
            IHttpContextAccessor httpContextAccessor,
            ILogger<RegisterHospitalUserRequirementHandler> logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            RegisterHospitalUserRequirement requirement,
            RegisterHospitalUserInput resource)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            resource = resource ?? throw new ArgumentNullException(nameof(resource));
            this.logger.LogInformation("Checking if user can register a hospital user");

            var role = context.User.FindFirst(ClaimTypes.Role);

            if (role.Value.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }

            if (role.Value.Equals(AppUserType.Hospital.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.HospitalClaimType).Value.Equals(resource.HospitalId))
            {
                context.Succeed(requirement);
                return;
            }

            var correlationId = this.httpContextAccessor.HttpContext.TraceIdentifier;
            var hospital = await this.domainBus.ExecuteAsync(new FindHospitalBySlugOrIdQuery(correlationId, id: resource.HospitalId));

            if (role.Value.Equals(AppUserType.Region.ToString(), StringComparison.OrdinalIgnoreCase) &&
                context.User.FindFirst(ClaimsPrincipalFactory.RegionClaimType).Value.Equals(hospital.RegionId.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
