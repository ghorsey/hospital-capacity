using Gah.HC.Domain;
using Gah.HC.Repository.Sql.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Gah.HC.Repository.Sql.Tests
{
    public class RegionRepositoryTests : SqliteTests<HospitalCapacityContext>
    {
        [Fact]
        public async Task MatchByNameMethodTest()
        {
            var regions = new List<Region>
            {
                new Region { Name = "California" },
                new Region { Name = "New York" },
            };

            await this.RunTestAsync(
                runner: async ctx =>
                {
                    IRegionRepository repo = new RegionRepository(ctx, this.MakeLogger<RegionRepository>());

                    var regions = await repo.MatchByName("o", default);
                    var caOnly = await repo.MatchByName("c", default);

                    Assert.Equal(2, regions.Count);
                    Assert.Single(caOnly);

                    Assert.Equal(regions[0].Id, caOnly[0].Id);
                },
                setup: async ctx =>
                {
                    await ctx.AddRangeAsync(regions);
                    await ctx.SaveChangesAsync();

                });

        }
    }
}
