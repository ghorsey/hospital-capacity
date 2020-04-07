namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// Interface <c>IQuery</c>
    /// Implements the <see cref="MediatR.IRequest{TResponse}" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IRequest{TResponse}" />
    public interface IDomainQuery<out TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; }
    }
}
