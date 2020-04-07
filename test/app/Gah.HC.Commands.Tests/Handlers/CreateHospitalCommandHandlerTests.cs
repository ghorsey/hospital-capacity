namespace Gah.HC.Commands.Tests.Handlers
{
    using Gah.HC.Commands.Handlers;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class CreateHospitalCommandHandlerTests
    {
        [Fact]
        public async Task HandleTest()
        {
            var h = new Hospital
            {
                BedsInUse = 75,
                BedCapacity = 100,
            };

            var hospitalRepo = new Mock<IHospitalRepository>(MockBehavior.Strict);
            hospitalRepo.Setup(r => r.AddAsync(h)).Returns(Task.CompletedTask);

            var capacityRepo = new Mock<IHospitalCapacityRepository>(MockBehavior.Strict);

            capacityRepo.Setup(
                r => r.AddAsync(
                    It.Is<HospitalCapacity>(c => c.PercentOfUsage == 75 && c.BedCapacity == 100 && c.BedsInUse == 75)))
                .Returns(Task.CompletedTask);

            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.HospitalRepository).Returns(hospitalRepo.Object);
            uowMock.SetupGet(u => u.HospitalCapacityRepository).Returns(capacityRepo.Object);
            uowMock.Setup(u => u.CommitAsync()).Returns(Task.FromResult(1));

            var handler = new CreateHospitalCommandHandler(uowMock.Object, new Mock<ILogger<CreateHospitalCommandHandler>>(MockBehavior.Loose).Object);

            await handler.Handle(new CreateHospitalCommand(h, "cor"), default);
            hospitalRepo.VerifyAll();
        }
    }
}
