namespace Gah.HC.Queries
{
    using System.Collections.Generic;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class MatchRegionByName.
    /// </summary>
    public class MatchRegionByNameQuery : IDomainQuery<List<Region>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRegionByNameQuery"/> class.
        /// </summary>
        /// <param name="partialName">The partial name.</param>
        public MatchRegionByNameQuery(string partialName)
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
