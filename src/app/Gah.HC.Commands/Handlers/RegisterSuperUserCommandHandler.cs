namespace Gah.HC.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands.Exceptions;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class RegisterSuperUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{RegisterSuperUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{RegisterSuperUserCommand}" />
    public class RegisterSuperUserCommandHandler : DomainCommandHandlerBase<RegisterSuperUserCommand>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterSuperUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userManager.</exception>
        public RegisterSuperUserCommandHandler(UserManager<AppUser> userManager, ILogger<RegisterSuperUserCommandHandler> logger)
            : base(logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException">command.</exception>
        /// <exception cref="UserCreationException">Failed to create a super user.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override async Task Handle(RegisterSuperUserCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation("Registering super user");

            var user = new AppUser
            {
                UserName = command.Email,
                UserType = AppUserType.Admin,
            };
            var result = await this.userManager.CreateAsync(user, command.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                throw new UserCreationException("Failed to create a super user", result.Errors);
            }
        }
    }
}
