using Gah.HC.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gah.HC.Queries.Tests
{
    /// <summary>
    /// Class UtilityExtensions.
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Makes the logger.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>ILogger&lt;T&gt;.</returns>
        public static ILogger<T> MakeLogger<T>(this object o)
        {
            return new Mock<ILogger<T>>(MockBehavior.Loose).Object;
        }

        /// <summary>
        /// Makes the logger factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>ILoggerFactory.</returns>
        public static ILoggerFactory MakeLoggerFactory<T>(this object o)
        {
            var factoryMock = new Mock<ILoggerFactory>(MockBehavior.Strict);

            factoryMock.SetupGet(f => f.CreateLogger(typeof(T)));

            return factoryMock.Object;
        }


        /// <summary>
        /// Makes the uow.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="regionRepostiry">The region repostiry.</param>
        /// <returns>IHospitalCapacityUow.</returns>
        public static IHospitalCapacityUow MakeUow(
            this object o,
            IRegionRepository regionRepostiry = null)
        {
            var uow = new Mock<IHospitalCapacityUow>(MockBehavior.Strict);

            if(regionRepostiry != null)
            {
                uow.SetupGet(u => u.RegionRepository).Returns(regionRepostiry);
            }


            return uow.Object;

        }
    }
}
