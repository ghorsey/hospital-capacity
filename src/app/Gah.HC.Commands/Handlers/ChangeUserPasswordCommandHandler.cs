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
    /// Class ChangeUserPasswordCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{ChangeUserPasswordCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{ChangeUserPasswordCommand}" />
    public class ChangeUserPasswordCommandHandler : DomainCommandHandlerBase<ChangeUserPasswordCommand>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserPasswordCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public ChangeUserPasswordCommandHandler(UserManager<AppUser> userManager, ILogger logger)
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
        /// <exception cref="Gah.HC.Commands.Exceptions.ChangePasswordException">Failed to change the user's password.</exception>
        public override async Task Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation("Changing the user's password");

            var result = await this.userManager.ChangePasswordAsync(command.User, command.CurrentPassword, command.NewPassword).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                throw new ChangePasswordException("Failed to change the user's password", result.Errors);
            }
        }
    }
}
