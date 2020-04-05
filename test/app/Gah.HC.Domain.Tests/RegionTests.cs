namespace Gah.HC.Domain.Tests
{
    using Xunit;

    public class RegionTests
    {
        [Fact]
        public void GenerateSlugTests()
        {
            var r = new Region
            {
                Name = "New York City",
            };


            Assert.Equal("new-york-city", r.Slug);
        }
    }
}
