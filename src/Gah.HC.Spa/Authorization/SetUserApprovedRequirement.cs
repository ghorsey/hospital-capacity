namespace Gah.HC.Spa.Authorization
{
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Class SetUserApprovedRequirement.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class SetUserApprovedRequirement : IAuthorizationRequirement
    {
    }
}
