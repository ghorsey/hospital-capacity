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
    /// Class HospitalRepository.
    /// Implements the <see cref="Gah.HC.Repository.Sql.RepositoryBase{Hospital, Guid, HospitalCapacityContext}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.Sql.RepositoryBase{Hospital, Guid, HospitalCapacityContext}" />
    public class HospitalRepository : RepositoryBase<Hospital, Guid, HospitalCapacityContext>, IHospitalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HospitalRepository(HospitalCapacityContext context, ILogger<HospitalRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Finds the by slug.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns>Task&lt;Hospital&gt;.</returns>
        public Task<Hospital> FindBySlugAsync(string slug)
        {
            this.Logger.LogInformation($"Finding hospital by slug: {slug}");

            return this.Entities.FirstOrDefaultAsync(h => h.Slug == slug);
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
            bool? isCovid = null)
        {
            this.Logger.LogInformation($"Finding hospitals for region: {regionId}, name: {name}, city: {city}, state: {state}, postal code: {postalCode}, capacity: {bedCapacity}, in use: {bedsInUse}");

            var q = this.Entities.AsQueryable();

            if (regionId != null && regionId.Value != Guid.Empty)
            {
                q = q.Where(e => e.RegionId == regionId);
            }

            if (percentageAvailable != -1)
            {
                q = q.Where(e => e.PercentageAvailable == percentageAvailable);
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
    }
}
