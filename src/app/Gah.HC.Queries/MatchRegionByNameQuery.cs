namespace Gah.HC.Queries
{
    using System.Collections.Generic;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class MatchRegionByName.
    /// </summary>
    public class MatchRegionByNameQuery : DomainQuery<List<Region>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRegionByNameQuery" /> class.
        /// </summary>
        /// <param name="partialName">The partial name.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        public MatchRegionByNameQuery(string partialName, string correlationId)
            : base(correlationId)
        {
            this.PartialName = partialName;
        }

        /// <summary>
        /// Gets the partial name.
        /// </summary>
        /// <value>The partial name.</value>
        public string PartialName { get;  }
    }
}
