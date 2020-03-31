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
        /// <summary>
        /// Configures the entity of type <see cref="AppUser"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email);
            builder.Property(e => e.PasswordHash);
            builder.Property(e => e.IsSiteAdmin);
            builder.Property(e => e.CreatedOn);
            builder.Property(e => e.UpdatedOn);
        }
    }
}
