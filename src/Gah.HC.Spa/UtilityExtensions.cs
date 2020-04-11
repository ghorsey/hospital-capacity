namespace Gah.HC.Spa
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Queries;

    /// <summary>
    /// Class UtilityExtensions.
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Makes the find region identifier or slug query.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="slugOrId">The slug or identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>FindRegionByIdOrSlugQuery.</returns>
        public static FindRegionByIdOrSlugQuery MakeFindRegionIdOrSlugQuery(this IDomainBus domainBus, string slugOrId, string correlationId)
        {
            if (Guid.TryParse(slugOrId, out var id))
            {
                return new FindRegionByIdOrSlugQuery(correlationId, id);
            }

            return new FindRegionByIdOrSlugQuery(correlationId, slug: slugOrId);
        }

        /// <summary>
        /// Finds the hospital by slug or identifier query.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="slugOrId">The slug or identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>FindHospitalBySlugOrIdQuery.</returns>
        public static FindHospitalBySlugOrIdQuery MakeFindHospitalBySlugOrIdQuery(this IDomainBus domainBus, string slugOrId, string correlationId)
        {
            if (Guid.TryParse(slugOrId, out var id))
            {
                return new FindHospitalBySlugOrIdQuery(correlationId, id);
            }

            return new FindHospitalBySlugOrIdQuery(correlationId, slug: slugOrId);
        }
    }
}
