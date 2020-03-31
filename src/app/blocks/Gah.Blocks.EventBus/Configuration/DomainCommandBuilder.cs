namespace TS.Blocks.DomainCommands.Configuration
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using TS.Blocks.DomainCommands;

    /// <summary>
    /// Class <c>CommandBuilder</c>.
    /// Implements the <see cref="IDomainCommandBuilder" />.
    /// </summary>
    /// <seealso cref="IDomainCommandBuilder" />
    public class DomainCommandBuilder : IDomainCommandBuilder
    {
        /// <summary>
        /// The services.
        /// </summary>
        private readonly IServiceCollection services;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainCommandBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public DomainCommandBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <typeparam name="TCommandHandler">The type of the t command handler.</typeparam>
        /// <returns>A/an <c>ICommandBuilder</c>.</returns>
        public IDomainCommandBuilder AddCommand<TCommand, TCommandHandler>()
            where TCommand : IDomainCommand
            where TCommandHandler : class,  IDomainCommandHandler<TCommand>
        {
            this.services.AddScoped<IRequestHandler<TCommand, Unit>, TCommandHandler>();

            return this;
        }
    }
}
