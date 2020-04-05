namespace Gah.HC.Spa
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Class ClaimsPrincipalFactory.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory{AppUser}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory{AppUser}" />
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsPrincipalFactory"/> class.
        /// </summary>
        /// <param name="userManager">The <see cref="Microsoft.AspNetCore.Identity.UserManager{T}" /> to retrieve user information from.</param>
        /// <param name="optionsAccessor">The configured <see cref="Microsoft.AspNetCore.Identity.IdentityOptions" />.</param>
        public ClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        /// <inheritdoc/>
        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            user = user ?? throw new ArgumentNullException(nameof(user));

            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserType.ToString()));
            identity.AddClaim(new Claim("HospitalId", user.HospitalId?.ToString() ?? string.Empty));
            identity.AddClaim(new Claim("RegionId", user.RegionId?.ToString() ?? string.Empty));

            return identity;
        }
    }
}
