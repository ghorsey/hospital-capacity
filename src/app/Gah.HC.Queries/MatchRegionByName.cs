namespace Gah.HC.Queries
{
    using System.Collections.Generic;
    using Gah.Blocks.EventBus;
    using Gah.HC.Domain;

    /// <summary>
    /// Class MatchRegionByName.
    /// </summary>
    public class MatchRegionByName : IDomainQuery<List<Region>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRegionByName"/> class.
        /// </summary>
        /// <param name="partialName">The partial name.</param>
        public MatchRegionByName(string partialName)
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
