namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalViewRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{HospitalView, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{HospitalView, Guid, HospitalCapacityContext}" />
    public class HospitalViewRepository : RepositoryBase<HospitalView, Guid, HospitalCapacityContext>, IHospitalViewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalViewRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HospitalViewRepository(
            HospitalCapacityContext context,
            ILogger<RepositoryBase<HospitalView, Guid, HospitalCapacityContext>> logger)
            : base(context, logger)
        {
        }

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
        /// <param name="percentOfUse">The percentage available.</param>
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
            bool? isCovid = null)
        {
            this.Logger.LogInformation($"Finding hospitals for region: {regionId}, name: {name}, city: {city}, state: {state}, postal code: {postalCode}, capacity: {bedCapacity}, in use: {bedsInUse}");

            var q = this.Entities.AsQueryable();

            if (regionId != null && regionId.Value != Guid.Empty)
            {
                q = q.Where(e => e.RegionId == regionId);
            }

            if (percentOfUse != -1)
            {
                q = q.Where(e => e.PercentOfUsage == percentOfUse);
            }

            if (isCovid != null)
            {
                q = q.Where(e => e.IsCovid == isCovid.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                q = q.Where(e => EF.Functions.Like(e.Name, $"%{name}%"));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                q = q.Where(e => EF.Functions.Like(e.City, $"%{city}%"));
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                q = q.Where(e => e.State == state);
            }

            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                q = q.Where(e => EF.Functions.Like(e.PostalCode, $"%{postalCode}%"));
            }

            if (bedsInUse >= 0)
            {
                q = q.Where(e => e.BedsInUse == bedsInUse);
            }

            if (bedCapacity != 0)
            {
                q = q.Where(e => e.BedCapacity == bedCapacity);
            }

            return q.ToListAsync();
        }

        /// <summary>
        /// Hospitals the view exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> HospitalViewExists(Guid id)
        {
            this.Logger.LogInformation($"checking if hospital view exists for {id}");

            return await this.Entities.CountAsync(e => e.Id == id).ConfigureAwait(false) > 0;
        }
    }
}
