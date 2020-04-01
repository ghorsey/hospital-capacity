namespace Gah.HC.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Region.
    /// Implements the <see cref="Entity{Guid}" />.
    /// </summary>
    /// <seealso cref="Entity{Guid}" />
    public class Region : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        public Region()
            : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Region(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        [Required]
        [MaxLength(50)]
        public string Name { get; } = string.Empty;
    }
}
