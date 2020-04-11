namespace Gah.HC.Spa.Tests.Controllers
{
    using AutoMapper;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Controllers;
    using Gah.HC.Spa.Models.Hospitals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    public class HospitalsControllerTests
    {

        [Fact(Skip = "Fix this UT")]
        public async Task CreateHospitalAsyncTest()
        {
            var input = new CreateHospitalInput
            {
                Address1 = "address1",
            };

            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);
            domainBusMock.Setup(b => b.ExecuteAsync(It.Is<CreateHospitalCommand>(c => c.Hospital.Address1 == input.Address1), default))
                .Returns(Task.CompletedTask);

            var c = new HospitalsController(
                new Mock<IAuthorizationService>(MockBehavior.Strict).Object,
                domainBusMock.Object,
                new Mock<IMapper>(MockBehavior.Strict).Object,
                new Mock<ILogger<HospitalsController>>().Object);
            c.SetDefaultContext();

            var response = await c.CreateHospitalAsync(input, default) as OkObjectResult;

            Assert.NotNull(response);

            var result = response.Value as Result<Hospital>;

            Assert.NotNull(result);
            Assert.Equal(input.Address1, result.Value.Address1);

            domainBusMock.VerifyAll();
        }

        [Fact]
        public async Task GetHospitalAsyncGuidTest()
        {
            var input = Guid.NewGuid();
            var hospital = new Hospital(input);

            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);

            domainBusMock.Setup(b => b.ExecuteAsync(It.Is<FindHospitalBySlugOrIdQuery>(q => q.Id == input), default))
                .ReturnsAsync(hospital);

            var c = new HospitalsController(
                new Mock<IAuthorizationService>(MockBehavior.Strict).Object,
                domainBusMock.Object,
                new Mock<IMapper>(MockBehavior.Strict).Object,
                new Mock<ILogger<HospitalsController>>().Object);
            c.SetDefaultContext();


            var response = await c.GetHospitalAsync(input.ToString(), default) as OkObjectResult;

            Assert.NotNull(response);

            var result = response.Value as Result<Hospital>;

            Assert.NotNull(result);

            Assert.Same(hospital, result.Value);

            domainBusMock.VerifyAll();
        }

        [Fact]
        public async Task GetHospitalAsyncSlugTest()
        {
            var input = "slug";
            var hospital = new Hospital();

            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);

            domainBusMock.Setup(b => b.ExecuteAsync(It.Is<FindHospitalBySlugOrIdQuery>(q => q.Slug == input), default))
                .ReturnsAsync(hospital);

            var c = new HospitalsController(
                new Mock<IAuthorizationService>(MockBehavior.Strict).Object,
                domainBusMock.Object,
                new Mock<IMapper>(MockBehavior.Strict).Object,
                new Mock<ILogger<HospitalsController>>().Object);
            c.SetDefaultContext();

            var response = await c.GetHospitalAsync(input.ToString(), default) as OkObjectResult;

            Assert.NotNull(response);

            var result = response.Value as Result<Hospital>;

            Assert.NotNull(result);

            Assert.Same(hospital, result.Value);

            domainBusMock.VerifyAll();
        }

        [Fact]
        public async Task FindHospitalsAsyncTest()
        {
            var regionId = Guid.NewGuid();

            var expected = new List<HospitalView>();

            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);
            domainBusMock.Setup(b => b.ExecuteAsync(It.Is<FindHospitalsQuery>(q => q.RegionId == regionId), default))
                .ReturnsAsync(expected);

            var c = new HospitalsController(
                new Mock<IAuthorizationService>(MockBehavior.Strict).Object,
                domainBusMock.Object,
                new Mock<IMapper>(MockBehavior.Strict).Object,
                new Mock<ILogger<HospitalsController>>().Object);
            c.SetDefaultContext();

            var response = await c.GetHospitalsAsync(regionId) as OkObjectResult;
            Assert.NotNull(response);

            var result = response.Value as Result<List<HospitalView>>;

            Assert.NotNull(result);

            Assert.Same(expected, result.Value);

            domainBusMock.VerifyAll();
        }
    }
}
