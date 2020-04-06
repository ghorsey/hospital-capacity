namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IHospitalRepository
    /// Implements the <see cref="Gah.HC.Repository.IRepository{Hospital, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{Hospital, Guid}" />
    public interface IHospitalRepository : IRepository<Hospital, Guid>
    {
        /// <summary>
        /// Finds the hospitals asynchronous.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="bedCapacity">The bed capacity.</param>
        /// <param name="bedsInUse">The beds in use.</param>
        /// <param name="percentageAvailable">The percentage available.</param>
        /// <param name="isCovid">if set to <c>true</c> [is covid].</param>
        /// <returns>Task&lt;List&lt;Hospital&gt;&gt;.</returns>
        public Task<List<Hospital>> FindHospitalsAsync(
            Guid? regionId = null,
            string name = "",
            string city = "",
            string state = "",
            string postalCode = "",
            int bedCapacity = 0,
            int bedsInUse = -1,
            int percentageAvailable = -1,
            bool? isCovid = null);

        /// <summary>
        /// Finds the by slug.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns>Task&lt;Hospital&gt;.</returns>
        public Task<Hospital> FindBySlugAsync(string slug);
    }
}
