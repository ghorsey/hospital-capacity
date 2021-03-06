﻿namespace Gah.HC.Repository.Sql.Data.Configuration
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class HospitalConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Hospital}" />.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Hospital}" />
    public class HospitalConfig : IEntityTypeConfiguration<Hospital>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Hospital"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        /// <exception cref="ArgumentNullException">builder.</exception>
        public void Configure(EntityTypeBuilder<Hospital> builder)
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

            builder.HasIndex(e => e.Slug)
                .HasName("AK_HOSPITAL_SLUG")
                .IsUnique();

            builder.HasIndex(e => e.RegionId)
                .HasName("IX_HOSPITAL_Region");

            builder.HasOne(e => e.Region);

            builder.HasIndex(e => e.Name);
            builder.HasIndex(e => e.PostalCode);
            builder.HasIndex(e => e.City);
            builder.HasIndex(e => e.State);
            builder.HasIndex(e => e.IsCovid);
        }
    }
}
