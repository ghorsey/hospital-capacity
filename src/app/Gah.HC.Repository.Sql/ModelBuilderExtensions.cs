namespace Gah.HC.Repository.Sql
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// Class ModelBuilderExtensions.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Sets the kind of the date time.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="kind">The kind.</param>
        /// <returns>ModelBuilder.</returns>
        /// <exception cref="ArgumentNullException">modelBuilder.</exception>
        public static ModelBuilder SetDateTimeKind(this ModelBuilder modelBuilder, DateTimeKind kind = DateTimeKind.Utc)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, kind));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                }
            }

            return modelBuilder;
        }
    }
}
