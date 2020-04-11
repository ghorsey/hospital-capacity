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
    /// Class SetUserPasswordCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{SetUserPasswordCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{SetUserPasswordCommand}" />
    public class SetUserPasswordCommandHandler : DomainCommandHandlerBase<SetUserPasswordCommand>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserPasswordCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">userManager.</exception>
        public SetUserPasswordCommandHandler(UserManager<AppUser> userManager, ILogger<SetUserPasswordCommand> logger)
            : base(logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">command.</exception>
        /// <exception cref="SetPasswordException">Failed setting the user password.</exception>
        public override async Task Handle(SetUserPasswordCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            this.Logger.LogInformation("Setting user password on their behalf");

            var token = await this.userManager.GeneratePasswordResetTokenAsync(command.User).ConfigureAwait(false);

            var response = await this.userManager.ResetPasswordAsync(command.User, token, command.Password).ConfigureAwait(false);

            if (!response.Succeeded)
            {
                throw new SetPasswordException("Failed setting the user password", response.Errors);
            }
        }
    }
}
