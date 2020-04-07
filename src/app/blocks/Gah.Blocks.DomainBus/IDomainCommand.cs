namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// Interface <c>ICommand</c>.
    /// Implements the <see cref="MediatR.IRequest" />.
    /// </summary>
    /// <seealso cref="MediatR.IRequest" />
    public interface IDomainCommand : IRequest
    {
        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        string CorrelationId { get; }
    }
}
