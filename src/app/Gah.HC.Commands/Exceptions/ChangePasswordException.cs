namespace Gah.HC.Commands.Exceptions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Class ChangePasswordException.
    /// Implements the <see cref="System.Exception" />.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ChangePasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        /// <exception cref="ArgumentNullException">errorsu.</exception>
        public ChangePasswordException(string message, IEnumerable<IdentityError> errors)
            : base(message)
        {
            this.Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public ChangePasswordException(string message, IEnumerable<IdentityError> errors, Exception innerException)
            : base(message, innerException)
        {
            this.Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordException"/> class.
        /// </summary>
        public ChangePasswordException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ChangePasswordException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public ChangePasswordException(string message, Exception innerException)
           : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<IdentityError> Errors { get; } = new List<IdentityError>();
    }
}
