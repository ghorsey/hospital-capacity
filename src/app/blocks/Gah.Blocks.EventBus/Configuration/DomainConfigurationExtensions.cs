namespace TS.Blocks.DomainCommands.Configuration
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using TS.Blocks.DomainCommands;

    /// <summary>
    /// Class <c>ConfigurationExtensions</c>.
    /// </summary>
    public static class DomainConfigurationExtensions
    {
        /// <summary>
        /// Adds the command support.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>A/an <c>IServiceCollection</c>.</returns>
        public static IDomainCommandBuilder AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => sp.GetService);

            services.AddScoped<IDomainCommandBus, DomainCommandBus>();

            return new DomainCommandBuilder(services);
        }
    }
}
