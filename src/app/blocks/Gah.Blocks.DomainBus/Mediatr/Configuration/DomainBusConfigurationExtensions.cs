namespace Gah.Blocks.DomainBus.Configuration
{
    using Gah.Blocks.DomainBus.Mediatr;
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class <c>ConfigurationExtensions</c>.
    /// </summary>
    public static class DomainBusConfigurationExtensions
    {
        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>A/an <c>IQueryBuilder</c>.</returns>
        public static IDomainBusBuilder AddDomainBus(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => sp.GetService);

            services.AddScoped<IDomainBus, DomainBusMediator>();

            return new DomainBusBuilder(services);
        }
    }
}
