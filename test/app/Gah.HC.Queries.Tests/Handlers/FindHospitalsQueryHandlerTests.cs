namespace Gah.HC.Queries.Tests.Handlers
{
    using Gah.HC.Domain;
    using Gah.HC.Queries.Handlers;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class FindHospitalsQueryHandlerTests
    {
        [Fact]
        public async Task HandlerTest()
        {
            var request = new FindHospitalsQuery(
                "cor",
                Guid.NewGuid(),
                "name",
                "city",
                "state",
                "postal",
                100,
                75,
                75,
                true);
            var expected = new List<HospitalView>();

            var repoMock = new Mock<IHospitalViewRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.FindHospitalsAsync(
                request.RegionId,
                request.Name,
                request.City,
                request.State,
                request.PostalCode,
                request.BedCapacity,
                request.BedsInUse,
                request.PercentOfUse,
                request.IsCovid))
                .ReturnsAsync(expected);

            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.HospitalViewRepository).Returns(repoMock.Object);

            var h = new FindHospitalsQueryHandler(uowMock.Object, new Mock<ILogger<FindHospitalsQueryHandler>>(MockBehavior.Loose).Object);

            var response = await h.Handle(request, default);

            Assert.Same(expected, response);

            repoMock.VerifyAll();
            uowMock.VerifyAll();
        }
    }
}
