namespace Gah.HC.Repository.Sql.Data.Configuration
{
    using System;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class AppUserConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{AppUser}" />.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{AppUser}" />
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Property(e => e.IsApproved)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
