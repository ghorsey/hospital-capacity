namespace Gah.HC.Repository.Sql.Data.Configuration
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <inheritdoc />
    public class HosptialViewConfig : IEntityTypeConfiguration<HospitalView>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<HospitalView> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Address1);
            builder.Property(e => e.Address2);
            builder.Property(e => e.BedCapacity);
            builder.Property(e => e.BedsInUse);
            builder.Property(e => e.City);
            builder.Property(e => e.CreatedOn);
            builder.Property(e => e.UpdatedOn);
            builder.Property(e => e.Name);
            builder.Property(e => e.PercentOfUsage);
            builder.Property(e => e.PostalCode);
            builder.Property(e => e.Phone);
            builder.Property(e => e.Slug);
            builder.Property(e => e.State);
            builder.Property(e => e.IsCovid);
            builder.Property(e => e.RegionName);

            builder.HasIndex(e => e.Slug)
                .HasName("AK_HOSPITALVIEW_SLUG")
                .IsUnique();

            builder.HasIndex(e => e.RegionId)
                .HasName("IX_HOSPITALVIEW_Region");

            builder.HasIndex(e => e.Name);
            builder.HasIndex(e => e.PostalCode);
            builder.HasIndex(e => e.City);
            builder.HasIndex(e => e.State);
            builder.HasIndex(e => e.IsCovid);
        }
    }
}
