namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class AppUserRepository.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore{AppUser}" />
    /// Implements the <see cref="Gah.HC.Repository.IAppUserRepository" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore{AppUser}" />
    /// <seealso cref="Gah.HC.Repository.IAppUserRepository" />
    public class AppUserRepository : UserStore<AppUser>, IAppUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository" /> class.
        /// </summary>
        /// <param name="context">The <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
        /// <param name="describer">The <see cref="Microsoft.AspNetCore.Identity.IdentityErrorDescriber" />.</param>
        public AppUserRepository(HospitalCapacityContext context, IdentityErrorDescriber? describer = null)
            : base(context, describer)
        {
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppUser&gt;.</returns>
        public Task<List<AppUser>> FindByAsync(Guid? regionId = null, Guid? hospitalId = null, CancellationToken cancellationToken = default)
        {
            var q = this.Users;

            if (regionId != null)
            {
                q = q.Where(e => e.RegionId == regionId);
            }

            if (hospitalId != null)
            {
                q = q.Where(e => e.HospitalId == hospitalId);
            }

            q = q.OrderBy(q => q.UserName);

            return q.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Sets the user authorized.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isApproved">if set to <c>true</c> the user is approved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task SetUserIsApprovedAsync(AppUser user, bool isApproved, CancellationToken cancellationToken = default)
        {
            user = user ?? throw new ArgumentNullException(nameof(user));

            user.IsApproved = isApproved;

            await this.UpdateAsync(user).ConfigureAwait(false);
            await this.Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
