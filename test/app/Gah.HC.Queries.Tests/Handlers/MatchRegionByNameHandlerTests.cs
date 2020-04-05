namespace Gah.HC.Queries.Tests.Handlers
{
    using Gah.HC.Domain;
    using Gah.HC.Queries.Handlers;
    using Gah.HC.Repository;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class MatchRegionByNameHandlerTests
    {
        [Fact]
        public async Task HandleMethodTest() {
            var result = new List<Region>();
            var query = new MatchRegionByName("partial");
            var regionRepoMock = new Mock<IRegionRepository>(MockBehavior.Strict);
            regionRepoMock.Setup(r => r.MatchByName(query.PartialName, default))
                .ReturnsAsync(result);

            var handler = new MatchRegionByNameHandler(this.MakeUow(regionRepoMock.Object), this.MakeLogger<MatchRegionByNameHandler>());

            var response = await handler.Handle(query, default);

            Assert.NotNull(response);

            Assert.Same(result, response);
        }
    }
}
