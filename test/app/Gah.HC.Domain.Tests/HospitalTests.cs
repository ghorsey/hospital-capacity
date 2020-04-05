namespace Gah.HC.Domain.Tests
{
    using Xunit;

    public class HospitalTests
    {
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
                State = state,
            };

            Assert.Equal(expectedSlug, hospital.Slug);
        }
    }
}
