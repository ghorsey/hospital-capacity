namespace Gah.HC.Commands.Tests.Handlers
{
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands.Handlers;
    using Gah.HC.Domain;
    using Gah.HC.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class RegisterRegionUserCommandHandlerTests
    {
        [Fact(Skip = "Fix later")]
        public async Task HandleMethodTest()
        {
            var command = new RegisterRegionUserCommand(
                "who@where.com",
                "password",
                "region",
                "cor");
            var loggerMock = new Mock<ILogger<RegisterRegionUserCommandHandler>>(MockBehavior.Loose);

            var regionRepositoryMock = new Mock<IRegionRepository>(MockBehavior.Strict);
            regionRepositoryMock.Setup(r => r.AddAsync(It.Is<Region>(g => g.Name == command.RegionName)))
                .Returns(Task.CompletedTask);
            var uowMock = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);
            uowMock.SetupGet(u => u.RegionRepository).Returns(regionRepositoryMock.Object);
            uowMock.Setup(u => u.CommitAsync()).Returns(Task.FromResult(1));
            uowMock.Setup(u => u.ExecuteInResilientTransactionAsync(It.IsAny<Func<Task<bool>>>()))
                .Callback((Func<Task<bool>> f) => f())
                .Returns(Task.FromResult(true));

            var userStoreMock = new Mock<IUserStore<AppUser>>(MockBehavior.Strict);
            userStoreMock.Setup(m => m.Dispose());

            var optionsMock = new Mock<IOptions<IdentityOptions>>(MockBehavior.Strict);
            optionsMock.SetupGet(om => om.Value).Returns(new IdentityOptions());


            using var um = new UserManager<AppUser>(
                userStoreMock.Object,
                optionsMock.Object,
                new Mock<IPasswordHasher<AppUser>>(MockBehavior.Strict).Object,
                new List<IUserValidator<AppUser>>(),
                new List<IPasswordValidator<AppUser>>(),
                new Mock<ILookupNormalizer>(MockBehavior.Strict).Object,
                new IdentityErrorDescriber(),
                new Mock<IServiceProvider>(MockBehavior.Strict).Object,
                new Mock<ILogger<UserManager<AppUser>>>(MockBehavior.Loose).Object);

            var h = new RegisterRegionUserCommandHandler(uowMock.Object, um, new Mock<IDomainBus>(MockBehavior.Strict).Object, loggerMock.Object);


            await h.Handle(command, default);
        }
    }
}
