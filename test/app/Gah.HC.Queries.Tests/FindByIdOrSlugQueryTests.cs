namespace Gah.HC.Queries.Tests
{
    using System;
    using Xunit;

    public class FindByIdOrSlugQueryTests
    {
        [Fact]
        public void CannotCreateQueryWithBothArgsUnset()
        {
            Assert.Throws<ArgumentException>(() => new FindBySlugOrIdQuery("cor"));
        }
    }
}
