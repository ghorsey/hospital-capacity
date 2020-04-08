﻿namespace Gah.HC.Commands.Handlers
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
    /// Class RegisterHospitalUserCommandHandler.
    /// Implements the <see cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{RegisterHospitalUserCommand}" />.
    /// </summary>
    /// <seealso cref="Gah.Blocks.DomainBus.DomainCommandHandlerBase{RegisterHospitalUserCommand}" />
    public class RegisterHospitalUserCommandHandler : DomainCommandHandlerBase<RegisterHospitalUserCommand>
    {
        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHospitalUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// uow
        /// or
        /// userManager.
        /// </exception>
        public RegisterHospitalUserCommandHandler(
            UserManager<AppUser> userManager,
            ILogger<RegisterHospitalUserCommandHandler> logger)
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
        /// <exception cref="UserCreationException">Failed to create user.</exception>
        public override async Task Handle(RegisterHospitalUserCommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            this.Logger.LogInformation($"Regisering hospital user {command.Email} for hospital {command.HospitalId}");

            var user = new AppUser
            {
                UserName = command.Email,
                UserType = AppUserType.Hospital,
                HospitalId = command.HospitalId,
            };

            var result = await this.userManager.CreateAsync(user, command.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                throw new UserCreationException("Failed to create user", result.Errors);
            }
        }
    }
}