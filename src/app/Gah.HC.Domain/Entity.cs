namespace Gah.HC.Domain
{
    using Newtonsoft.Json;

    /// <summary>
    /// The base class for all entities.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class Entity<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TId}" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected Entity(TId id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty]
        public TId Id { get; protected set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? 0 * 17;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            var e = (Entity<TId>)obj;

            return e.Id?.Equals(this.Id) ?? false;
        }
    }
}
