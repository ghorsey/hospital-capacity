namespace Gah.HC.Spa.Authorization.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class AdminOnlyAccessRequirementHandler.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{AdminOnlyAccessRequirement}" />.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{AdminOnlyAccessRequirement}" />
    public class AdminOnlyAccessRequirementHandler : AuthorizationHandler<AdminOnlyAccessRequirement>
    {
        private readonly ILogger<AdminOnlyAccessRequirementHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminOnlyAccessRequirementHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public AdminOnlyAccessRequirementHandler(ILogger<AdminOnlyAccessRequirementHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>Task.</returns>
        /// <exception cref="ArgumentNullException">context.</exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOnlyAccessRequirement requirement)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger.LogInformation("Checking admin only access");

            var role = context.User.FindFirst(ClaimTypes.Role).Value;

            if (role.Equals(AppUserType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this.logger.LogInformation("The user is an admin");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            this.logger.LogInformation("Access denied");
            return Task.CompletedTask;
        }
    }
}
