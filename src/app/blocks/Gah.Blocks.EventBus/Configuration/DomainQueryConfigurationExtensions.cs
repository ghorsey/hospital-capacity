namespace TS.Blocks.DomainQueries.Configuration
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using TS.Blocks.DomainQueries;

    /// <summary>
    /// Class <c>ConfigurationExtensions</c>.
    /// </summary>
    public static class DomainQueryConfigurationExtensions
    {
        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>A/an <c>IQueryBuilder</c>.</returns>
        public static IDomainQueryBuilder AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => sp.GetService);

            services.AddScoped<IDomainQueryBus, DomainQueryBus>();

            return new DomainQueryBuilder(services);
        }
    }
}
