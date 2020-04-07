namespace Gah.Blocks.DomainBus.Configuration
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class <c>CommandBuilder</c>.
    /// Implements the <see cref="IDomainBusBuilder" />.
    /// </summary>
    /// <seealso cref="IDomainBusBuilder" />
    public class DomainBusBuilder : IDomainBusBuilder
    {
        /// <summary>
        /// The services.
        /// </summary>
        private readonly IServiceCollection services;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBusBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public DomainBusBuilder(IServiceCollection services)
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
        public IDomainBusBuilder AddCommand<TCommand, TCommandHandler>()
            where TCommand : IDomainCommand
            where TCommandHandler : class,  IDomainCommandHandler<TCommand>
        {
            this.services.AddScoped<IRequestHandler<TCommand, Unit>, TCommandHandler>();

            return this;
        }

        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <typeparam name="TQueryResponse">The type of the query response.</typeparam>
        /// <typeparam name="TQueryHandler">The type of the query handler.</typeparam>
        /// <returns>A/an <c>IQueryBuilder</c>.</returns>
        public IDomainBusBuilder AddQuery<TQuery, TQueryResponse, TQueryHandler>()
            where TQuery : IDomainQuery<TQueryResponse>
            where TQueryHandler : class, IDomainQueryHandler<TQuery, TQueryResponse>
        {
            this.services.AddScoped<IRequestHandler<TQuery, TQueryResponse>, TQueryHandler>();
            return this;
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the t event handler.</typeparam>
        /// <returns>A/an <c>IEventBuilder</c>.</returns>
        public IDomainBusBuilder AddEvent<TEvent, TEventHandler>()
            where TEvent : IDomainEvent
            where TEventHandler : class, IDomainEventHandler<TEvent>
        {
            this.services.AddScoped<INotificationHandler<TEvent>, TEventHandler>();

            return this;
        }
    }
}
