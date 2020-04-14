namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <summary>
    /// Class IAppUserViewRepository.
    /// Implements the <see cref="Gah.HC.Repository.IRepository{AppUserView, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{AppUserView, Guid}" />
    public interface IAppUserViewRepository : IRepository<AppUserView, Guid>
    {
        /// <summary>
        /// Finds the by asynchronous.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;AppUserView&gt;&gt;.</returns>
        Task<List<AppUserView>> FindByAsync(Guid? regionId = null, Guid? hospitalId = null, CancellationToken cancellationToken = default);
    }
}
