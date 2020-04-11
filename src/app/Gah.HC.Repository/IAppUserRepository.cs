namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IAppUserRepository.
    /// </summary>
    public interface IAppUserRepository
    {
        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppUser&gt;.</returns>
        Task<List<AppUser>> FindBy(Guid? regionId = null, Guid? hospitalId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the user authorized.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isApproved">if set to <c>true</c> the user is approved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task SetUserAuthorized(AppUser user, bool isApproved, CancellationToken cancellationToken = default);
    }
}
