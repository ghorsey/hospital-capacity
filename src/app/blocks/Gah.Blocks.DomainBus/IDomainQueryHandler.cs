namespace Gah.Blocks.DomainBus
{
    using MediatR;

    /// <summary>
    /// Interface <c>IQueryHandler</c>
    /// Implements the <see cref="MediatR.IRequestHandler{TQuery, TResponse}" />.
    /// </summary>
    /// <typeparam name="TQuery">The type of the t query.</typeparam>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="MediatR.IRequestHandler{TQuery, TResponse}" />
    public interface IDomainQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IDomainQuery<TResponse>
    {
    }
}
