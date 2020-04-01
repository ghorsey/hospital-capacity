namespace Gah.HC.Repository.Sql.Tests
{
    using Gah.HC.Repository.Sql.Data;
    using System.Threading.Tasks;
    using Xunit;

    public class MigrationsTest : SqliteTests<HospitalCapacityContext>
    {
        [Fact]
        public Task TestMigrationsAsync()
        {
            return this.RunTestAsync(
                runner: ctx =>
                {
                    return Task.CompletedTask;
                },
                setup: ctx =>
                {
                    return Task.CompletedTask;
                });
        }
    }
}
