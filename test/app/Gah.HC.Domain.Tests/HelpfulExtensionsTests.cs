
namespace Gah.HC.Domain.Tests
{
    using Xunit;

    public class HelpfulExtensionsTests
    {
        [Theory]
        [InlineData("this is a tEsT", "this-is-a-test")]
        public void StringToSlug(string input, string expected)
        {
            Assert.Equal(input.ToSlug(), expected);
        }
    }
}
