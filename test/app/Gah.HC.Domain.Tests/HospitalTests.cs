using Xunit;

namespace Gah.HC.Domain.Tests
{
    public class HospitalTests
    {
        /// <summary>
        /// Defines the test method CalculatePercentageMethod.
        /// </summary>
        /// <param name="total">The total.</param>
        /// <param name="inUse">The in use.</param>
        /// <param name="expectedPercentage">The expected percentage.</param>
        [Theory]
        [InlineData(100, 50, 50)]
        [InlineData(15485, 384, 2)]
        [InlineData(100, 150, 150)]
        public void CalculatePercentageAvailableMethodTest(int total, int inUse, int expectedPercentage)
        {
            var hospital = new Hospital
            {
                BedCapacity = total,
                BedsInUse = inUse
            };

            hospital.CalculatePercentageAvailable();


            Assert.Equal(expectedPercentage, hospital.PercentageAvailable);
        }
    }
}
