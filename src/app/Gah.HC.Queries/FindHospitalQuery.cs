namespace Gah.HC.Queries
{
    using System;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class FindHospitalQuery.
    /// Implements the <see cref="Gah.Blocks.EventBus.DomainQueryBase{Hospital}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.EventBus.DomainQueryBase{Hospital}" />
    public class FindHospitalQuery : DomainQueryBase<Hospital>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindHospitalQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="slug">The slug.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public FindHospitalQuery(string correlationId, Guid? id = null, string slug = "")
            : base(correlationId)
        {
            if (id == null && string.IsNullOrWhiteSpace(slug))
            {
                throw new ArgumentException($"Both id and slug cannot be null");
            }

            this.Slug = slug;
            this.Id = id;
        }

        /// <summary>
        /// Gets the slug.
        /// </summary>
        /// <value>The slug.</value>
        public string Slug { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; }
    }
}
