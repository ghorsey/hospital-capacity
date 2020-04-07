namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// Interface <c>IEventHandler</c>
    /// Implements the <see cref="MediatR.INotificationHandler{TEvent}" />.
    /// </summary>
    /// <typeparam name="TEvent">The type of the t event.</typeparam>
    /// <seealso cref="MediatR.INotificationHandler{TEvent}" />
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
