using Gah.HC.Repository.Sql.Data;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gah.HC.Repository.Sql.Tests
{
    public static class UtilityExtensions
    {
        /// <summary>
        /// Generates the logger factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_">The .</param>
        /// <returns>ILogger&lt;T&gt;.</returns>
        public static ILogger<T> MakeLogger<T>(this SqliteTests<HospitalCapacityContext> test)
        {
            var loggerMock = new Mock<ILogger<T>>(MockBehavior.Loose);
            return loggerMock.Object;
        }
    }
}
