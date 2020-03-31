namespace Gah.HC.Repository.Sql.Tests
{
    using Gah.HC.Domain;
    using Gah.HC.Repository.Sql.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class MigrationsTest : SqliteTests<HospitalCapacityContext>
    {
        private List<Hospital> DefaultHospitalData { get; } =new List<Hospital>
        {
            new Hospital()
            {
                Name = "hospital 1",
                Address1 = "hospital 1 address 1",
                Address2 = "hospital 1 address 2",
                BedCapacity = 100,
                BedsInUse = 50,
                City = "los angeles",
                PostalCode = "90066",
                State = "CA"
            },
            new Hospital()
            {
                Name = "hospital 2",
                Address1 = "hospital 2 address 1",
                Address2 = "hospital 2 address 2",
                BedCapacity = 100,
                BedsInUse = 50,
                City = "los angeles",
                PostalCode = "90066",
                State = "CA"
            },
        };

        [Fact]
        public Task TestMigrationsAsync()
        {
            return this.RunTestAsync(
                runner: async ctx =>
                {
                    var items = await ctx.Hospitals.ToListAsync();
                    var h1 = await ctx.Hospitals.FindAsync(this.DefaultHospitalData[0].Id);
                    Assert.Equal(DefaultHospitalData[0].Id, h1.Id);
                },
                setup: async ctx =>
                {
                    ctx.Hospitals.Add(this.DefaultHospitalData[0]);
                    await ctx.SaveChangesAsync();
                });
        }
    }
}
