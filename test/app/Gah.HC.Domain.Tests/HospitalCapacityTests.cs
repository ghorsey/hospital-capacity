namespace Gah.HC.Domain.Tests
{
    using Xunit;

    public class HospitalCapacityTests
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
            var hospital = new HospitalCapacity
            {
                BedCapacity = total,
                BedsInUse = inUse,
            };

            hospital.CalculatePercentOfUsage();

            Assert.Equal(expectedPercentage, hospital.PercentOfUsage);
        }
    }
}
