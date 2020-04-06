﻿namespace Gah.HC.Repository.Sql.Tests
{
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class HospitalRepositoryTests: SqliteTests<HospitalCapacityContext>
    {
        [Fact]
        public async Task FindHospitalsAsyncTest()
        {
            var region = new Region { Name = "Los angeles" };
            var hospitals = new List<Hospital>
            {
                new Hospital
                {
                    BedCapacity =150,
                    City = "city",
                    BedsInUse = 200,
                    Name = "My Name",
                    IsCovid = true,
                    RegionId = region.Id,
                    PostalCode = "90066",
                    State = "CA",
                    PercentageAvailable = 125
                },
                new Hospital
                {
                    BedCapacity =150,
                    City = "city",
                    BedsInUse = 200,
                    Name = "My Name Numba Two",
                    IsCovid = false,
                    RegionId = region.Id,
                    PostalCode = "90066",
                    State = "CA",
                    PercentageAvailable = 125
                }
            };

            await this.RunTestAsync(
                runner: async ctx =>
                {
                    var h = hospitals[0];
                    IHospitalRepository repo = new HospitalRepository(ctx, this.MakeLogger<HospitalRepository>());

                    var allItems = await repo.FindHospitalsAsync();

                    var oneItem = await repo.FindHospitalsAsync(
                        h.RegionId,
                        h.Name,
                        h.City,
                        h.State,
                        h.PostalCode,
                        h.BedCapacity,
                        h.BedsInUse,
                        h.PercentageAvailable,
                        h.IsCovid);

                    Assert.Equal(2, allItems.Count);
                    Assert.Single(oneItem);

                    Assert.Equal(h.Id, oneItem[0].Id);
                },
                setup: async ctx =>
                {
                    await ctx.AddAsync(region);
                    await ctx.AddRangeAsync(hospitals);
                    await ctx.SaveChangesAsync();
                });
            
        }
    }
}
