namespace Gah.HC.Repository.Sql.Data.Configuration
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class RegionConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Region}" />.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Region}" />
    public class RegionConfig : IEntityTypeConfiguration<Region>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name);
        }
    }
}
