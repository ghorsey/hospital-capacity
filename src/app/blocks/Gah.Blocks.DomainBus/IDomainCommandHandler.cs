namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// The CommandHandler interface.
    /// Implements the <see cref="IRequestHandler{TCommand}" />.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <seealso cref="IRequestHandler{TCommand}" />
    public interface IDomainCommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : IDomainCommand
    {
    }
}
