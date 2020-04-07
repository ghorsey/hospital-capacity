namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindBySlugOrIdQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQueryBase{Hospital}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQueryBase{Hospital}" />
    public class FindBySlugOrIdQuery : DomainQueryBase<Hospital>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindBySlugOrIdQuery"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="slug">The slug.</param>
        public FindBySlugOrIdQuery(string correlationId, Guid? id = null, string slug = "")
            : base(correlationId)
        {
            if ((id == null || id == Guid.Empty) && string.IsNullOrWhiteSpace(slug))
            {
                throw new ArgumentException("either an id or slug must be supplied");
            }

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
