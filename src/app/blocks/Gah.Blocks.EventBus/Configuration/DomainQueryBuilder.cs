namespace TS.Blocks.DomainQueries.Configuration
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using TS.Blocks.DomainQueries;

    /// <summary>
    /// The query builder.
    /// Implements the <see cref="IDomainQueryBuilder" />.
    /// </summary>
    /// <seealso cref="IDomainQueryBuilder" />
    public class DomainQueryBuilder : IDomainQueryBuilder
    {
        /// <summary>
        /// The services.
        /// </summary>
        private readonly IServiceCollection services;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainQueryBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public DomainQueryBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <typeparam name="TQueryResponse">The type of the query response.</typeparam>
        /// <typeparam name="TQueryHandler">The type of the query handler.</typeparam>
        /// <returns>A/an <c>IQueryBuilder</c>.</returns>
        public IDomainQueryBuilder AddQuery<TQuery, TQueryResponse, TQueryHandler>()
            where TQuery : IDomainQuery<TQueryResponse>
            where TQueryHandler : class, IDomainQueryHandler<TQuery, TQueryResponse>
        {
            this.services.AddScoped<IRequestHandler<TQuery, TQueryResponse>, TQueryHandler>();
            return this;
        }
    }
}
