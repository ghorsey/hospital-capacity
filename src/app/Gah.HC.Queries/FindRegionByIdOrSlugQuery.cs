namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindRegionByIdOrSlug.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryBase{Region}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryBase{Region}" />
    public class FindRegionByIdOrSlugQuery : DomainQueryBase<Region>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindRegionByIdOrSlugQuery" /> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="slug">The slug.</param>
        public FindRegionByIdOrSlugQuery(string correlationId, Guid? id = null, string slug = "")
            : base(correlationId)
        {
            this.Id = id;
            this.Slug = slug;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; }

        /// <summary>
        /// Gets the slug.
        /// </summary>
        /// <value>The slug.</value>
        public string Slug { get; }
    }
}
