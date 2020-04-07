using Gah.HC.Domain;
using Gah.HC.Queries.Handlers;
using Gah.HC.Repository;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Gah.HC.Queries.Tests.Handlers
{
    public class FindBySlugOrIdQueryHandlerTests
    {
        private Hospital hospital = new Hospital
        {
            Name = "hospital"
        };

        [Fact]
        public async Task HandleMethodByIdTest()
        {
            var logger = this.MakeLogger<FindBySlugOrIdQueryHandler>();
            var hospitalRepoMock = new Mock<IHospitalRepository>(MockBehavior.Strict);
            hospitalRepoMock.Setup(r => r.FindAsync(this.hospital.Id, default)).ReturnsAsync(hospital);
            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.HospitalRepository).Returns(hospitalRepoMock.Object);

            var h = new FindBySlugOrIdQueryHandler(uowMock.Object, logger);

            var result = await h.Handle(new FindBySlugOrIdQuery("cor", id: hospital.Id), default);


            Assert.Same(this.hospital, result);
        }

        [Fact]
        public async Task HandleMethodBySlugTest()
        {
            var logger = this.MakeLogger<FindBySlugOrIdQueryHandler>();
            var hospitalRepoMock = new Mock<IHospitalRepository>(MockBehavior.Strict);
            hospitalRepoMock.Setup(r => r.FindBySlugAsync(this.hospital.Slug, default)).ReturnsAsync(hospital);
            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.HospitalRepository).Returns(hospitalRepoMock.Object);

            var h = new FindBySlugOrIdQueryHandler(uowMock.Object, logger);

            var result = await h.Handle(new FindBySlugOrIdQuery("cor", slug: hospital.Slug), default);


            Assert.Same(this.hospital, result);
        }
    }
}
