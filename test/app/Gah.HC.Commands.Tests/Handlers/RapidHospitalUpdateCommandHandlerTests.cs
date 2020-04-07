namespace Gah.HC.Commands.Tests.Handlers
{
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands.Handlers;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class RapidHospitalUpdateCommandHandlerTests
    {
        [Fact(Skip = "Need to update")]
        public async Task HandleMethodTest()
        {
            var hospital = new Hospital();
            var command = new RapidHospitalUpdateCommand(hospital.Id, 25, 100, true);

            var domainBusMock = new Mock<IDomainBus>(MockBehavior.Strict);

            var hospRepo = new Mock<IHospitalRepository>(MockBehavior.Strict);
            hospRepo.Setup(r => r.FindAsync(command.Id, default))
                .ReturnsAsync(hospital);

            hospRepo.Setup(r => r.UpdateAsync(hospital))
                .Returns(Task.CompletedTask);

            var capRepo = new Mock<IHospitalCapacityRepository>(MockBehavior.Strict);
            capRepo.Setup(
                r => r.AddAsync(
                    It.Is<HospitalCapacity>(
                        c => c.BedCapacity == command.BedCapacity &&
                        c.BedsInUse == command.BedsInUse &&
                        c.PercentOfUsage == 25)))
                .Returns(Task.CompletedTask);

            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.Setup(u => u.HospitalCapacityRepository).Returns(capRepo.Object);
            uowMock.Setup(u => u.HospitalRepository).Returns(hospRepo.Object);

            uowMock.Setup(u => u.ExecuteInResilientTransactionAsync(It.IsAny<Func<Task<bool>>>()))
                .Callback((Func<Task<bool>> f) => f())
                .Returns(Task.FromResult(true));

            var handler = new RapidHospitalUpdateCommandHandler(uowMock.Object, domainBusMock.Object, new Mock<ILogger<RapidHospitalUpdateCommandHandler>>().Object);

            await handler.Handle(command, default);

            Assert.Equal(command.BedsInUse, hospital.BedsInUse);
            Assert.Equal(command.BedCapacity, hospital.BedCapacity);
            Assert.Equal(25, hospital.PercentOfUsage);
            Assert.Equal(command.IsCovid, hospital.IsCovid);

            uowMock.VerifyAll();
            hospRepo.VerifyAll();
            capRepo.VerifyAll();
        }
    }
}
