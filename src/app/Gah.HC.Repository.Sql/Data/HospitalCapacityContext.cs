namespace Gah.HC.Repository.Sql.Data
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class HospitalCapacityContext.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{AppUser}" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{AppUser}" />
    public class HospitalCapacityContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalCapacityContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public HospitalCapacityContext(DbContextOptions<HospitalCapacityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the hospitals.
        /// </summary>
        /// <value>The hospitals.</value>
        public virtual DbSet<Hospital>? Hospitals { get; private set; }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <value>The regions.</value>
        public virtual DbSet<Region>? Regions { get; private set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="Microsoft.EntityFrameworkCore.DbSet{T}" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <exception cref="ArgumentNullException">modelBuilder.</exception>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
