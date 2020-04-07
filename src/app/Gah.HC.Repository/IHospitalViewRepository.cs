namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IHospitalViewRepository
    /// Implements the <see cref="Gah.HC.Repository.IRepository{HospitalView, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{HospitalView, Guid}" />
    public interface IHospitalViewRepository : IRepository<HospitalView, Guid>
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
        /// <param name="percentOfUse">The percent of use.</param>
        /// <param name="isCovid">if set to <c>true</c> [is covid].</param>
        /// <returns>Task&lt;List&lt;Hospital&gt;&gt;.</returns>
        public Task<List<HospitalView>> FindHospitalsAsync(
            Guid? regionId = null,
            string name = "",
            string city = "",
            string state = "",
            string postalCode = "",
            int bedCapacity = 0,
            int bedsInUse = -1,
            int percentOfUse = -1,
            bool? isCovid = null);
    }
}
