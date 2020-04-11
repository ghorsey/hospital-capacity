namespace Gah.HC.Spa.Authorization
{
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Class SetUserPasswordRequirement.
    /// Implements the <see cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class SetUserPasswordRequirement : IAuthorizationRequirement
    {
    }
}
