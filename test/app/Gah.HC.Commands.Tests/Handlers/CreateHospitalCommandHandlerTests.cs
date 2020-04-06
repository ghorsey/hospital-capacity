namespace Gah.HC.Commands.Tests.Handlers
{
    using Gah.HC.Commands.Handlers;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Gah.HC.Repository.Sql;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class CreateHospitalCommandHandlerTests
    {
        [Fact(Skip ="Need to update now that we record the hospital capacity")]
        public async Task HandleTest()
        {
            var h = new Hospital();
            var repoMock = new Mock<IHospitalRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.AddAsync(h)).Returns(Task.CompletedTask);
            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.HospitalRepository).Returns(repoMock.Object);
            uowMock.Setup(u => u.CommitAsync()).Returns(Task.FromResult(1));

            var handler = new CreateHospitalCommandHandler(uowMock.Object, new Mock<ILogger<CreateHospitalCommandHandler>>(MockBehavior.Loose).Object);

            await handler.Handle(new CreateHospitalCommand(h, "cor"), default);
        }
    }
}
