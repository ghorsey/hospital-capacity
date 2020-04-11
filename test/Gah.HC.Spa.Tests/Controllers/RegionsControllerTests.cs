namespace Gah.HC.Spa.Tests.Controllers
{
    using Gah.Blocks.DomainBus;
    using Gah.HC.Spa.Controllers;
    using Gah.HC.Queries;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;
    using System.Collections.Generic;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;

    public class RegionsControllerTests
    {
        [Fact]
        public async Task FindReginosByPartialNameTests()
        {
            var expected = new List<Region>();
            var partialName = "partial";
            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);
            domainBusMock.Setup(s => s.ExecuteAsync(It.Is<MatchRegionByNameQuery>(q => q.PartialName == partialName), default))
                .ReturnsAsync(expected);

            var loggerMock = new Mock<ILogger<RegionsController>>(MockBehavior.Loose);

            var authServiceMock = new Mock<IAuthorizationService>(MockBehavior.Strict);

            var c = new RegionsController(
                domainBusMock.Object,
                authServiceMock.Object,
                new Mock<IMapper>(MockBehavior.Strict).Object,
                loggerMock.Object);
            c.SetDefaultContext();

            var response = await c.FindRegionsByPartialNameAsync(partialName, default) as OkObjectResult;
            Assert.NotNull(response);

            var result = response.Value as Result<List<Region>>;
            Assert.NotNull(result);
            Assert.Same(expected, result.Value);
        }
    }
}
