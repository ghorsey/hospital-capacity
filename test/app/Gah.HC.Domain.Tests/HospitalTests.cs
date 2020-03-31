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

        /// <summary>
        /// Hospitals the slug tests.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="expectedSlug">The expected slug.</param>
        [Theory]
        [InlineData("Mercy Hospital", "123 Nowhere Ln.", "Los Angeles", "CA", "mercy-hospital-123-nowhere-ln-los-angeles-ca")]
        public void HospitalSlugTests(string name, string address, string city, string state, string expectedSlug)
        {
            var hospital = new Hospital()
            {
                Name = name,
                Address1 = address,
                City = city,
                State = state
            };

            Assert.Equal(expectedSlug, hospital.Slug);
        }
    }
}
