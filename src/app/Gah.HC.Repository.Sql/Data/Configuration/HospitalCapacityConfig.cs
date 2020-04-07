namespace Gah.HC.Repository.Sql.Data.Configuration
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class HospitalCapacityConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{HospitalCapacity}" />.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{HospitalCapacity}" />
    public class HospitalCapacityConfig : IEntityTypeConfiguration<HospitalCapacity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<HospitalCapacity> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("HospitalCapacity");

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Hospital);

            builder.Property(e => e.BedCapacity)
                .IsRequired();

            builder.Property(e => e.BedsInUse)
                .IsRequired();

            builder.Property(e => e.PercentOfUsage)
                .IsRequired();

            builder.Property(e => e.CreatedOn)
                .IsRequired();

            builder.HasIndex(e => e.CreatedOn)
                .HasName("IX_HOSPITALCAPACITY_CREATEDON");
        }
    }
}
