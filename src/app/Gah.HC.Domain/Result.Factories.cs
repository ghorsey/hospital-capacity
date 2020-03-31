﻿namespace Gah.HC.Domain
{
    /// <summary>
    /// The API Result object factories.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Makes the unsuccessful.
        /// </summary>
        /// <typeparam name="T">The type of the API result value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// An unsuccessful API result object with the specified Data.
        /// </returns>
        public static Result<T> MakeUnsuccessfulResult<T>(this T value, string message)
        {
            return new Result<T>(value) { Success = false, Message = message };
        }

        /// <summary>
        /// Makes the unsuccessful result.
        /// </summary>
        /// <typeparam name="T">The type of the api result value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>An unsuccessful API result object.</returns>
        /// <remarks>The variation will set both the value and message with the same content.</remarks>
        public static Result<T> MakeUnsuccessfulResult<T>(this T value)
        {
            return new Result<T>(value)
                       {
                           Success = false,
                           Message = value?.ToString() ?? string.Empty,
                       };
        }

        /// <summary>
        /// Makes the unsuccessful.
        /// </summary>
        /// <typeparam name="T">The type of the API result value.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        /// An unsuccessful API result object of the specified type.
        /// </returns>
        public static Result<T> MakeUnsuccessfulResult<T>(string message)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return new Result<T>(default) { Success = false, Message = message };
#pragma warning restore CS8604 // Possible null reference argument.
        }

        /// <summary>
        /// Makes the successful.
        /// </summary>
        /// <typeparam name="T">The result value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A successful API Result.
        /// </returns>
        public static Result<T> MakeSuccessfulResult<T>(this T value)
        {
            return new Result<T>(value);
        }
    }
}
