namespace Gah.HC.Queries.Tests.Handlers
{
    using Gah.HC.Domain;
    using Gah.HC.Queries.Handlers;
    using Gah.HC.Repository;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class MatchRegionByNameQueryHandlerTests
    {
        [Fact]
        public async Task HandleMethodTest() {
            var result = new List<Region>();
            var query = new MatchRegionByNameQuery("partial", "cor");
            var regionRepoMock = new Mock<IRegionRepository>(MockBehavior.Strict);
            regionRepoMock.Setup(r => r.MatchByNameAsync(query.PartialName, default))
                .ReturnsAsync(result);

            var handler = new MatchRegionByNameQueryHandler(this.MakeUow(regionRepoMock.Object), this.MakeLogger<MatchRegionByNameQueryHandler>());

            var response = await handler.Handle(query, default);

            Assert.NotNull(response);

            Assert.Same(result, response);
        }
    }
}
