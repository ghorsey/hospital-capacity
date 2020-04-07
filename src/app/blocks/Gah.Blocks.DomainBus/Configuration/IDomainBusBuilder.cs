namespace Gah.Blocks.DomainBus.Configuration
{
    /// <summary>
    /// Interface <c>ICommandBuilder</c>.
    /// </summary>
    public interface IDomainBusBuilder
    {
        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <typeparam name="TCommandHandler">The type of the t command handler.</typeparam>
        /// <returns>A/an <c>ICommandBuilder</c>.</returns>
        IDomainBusBuilder AddCommand<TCommand, TCommandHandler>()
            where TCommand : IDomainCommand
            where TCommandHandler : class, IDomainCommandHandler<TCommand>;

        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <typeparam name="TQueryResponse">The type of the query response.</typeparam>
        /// <typeparam name="TQueryHandler">The type of the query handler.</typeparam>
        /// <returns>A/an <c>IQueryBuilder</c>.</returns>
        IDomainBusBuilder AddQuery<TQuery, TQueryResponse, TQueryHandler>()
            where TQuery : IDomainQuery<TQueryResponse>
            where TQueryHandler : class, IDomainQueryHandler<TQuery, TQueryResponse>;

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the t event handler.</typeparam>
        /// <returns>A/an <c>IEventBuilder</c>.</returns>
        IDomainBusBuilder AddEvent<TEvent, TEventHandler>()
            where TEvent : IDomainEvent
            where TEventHandler : class, IDomainEventHandler<TEvent>;
    }
}
