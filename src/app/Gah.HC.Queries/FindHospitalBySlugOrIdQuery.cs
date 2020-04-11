namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindBySlugOrIdQuery.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainQuery{Hospital}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainQuery{Hospital}" />
    public class FindHospitalBySlugOrIdQuery : DomainQuery<Hospital>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalBySlugOrIdQuery"/> class.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="slug">The slug.</param>
        public FindHospitalBySlugOrIdQuery(string correlationId, Guid? id = null, string slug = "")
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
