namespace Gah.HC.Repository.Sql.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class SqliteTests.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the t database context.</typeparam>
    public abstract class SqliteTests<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// run test as an asynchronous operation.
        /// </summary>
        /// <param name="runner">The runner.</param>
        /// <param name="setup">The setup.</param>
        /// <returns>Task.</returns>
        protected virtual async Task RunTestAsync(Func<TDbContext, Task> runner, Func<TDbContext, Task> setup = null)
        {
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<TDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = this.MakeDbContext(options))
                {
                    await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
                }

                if (setup != null)
                {
                    using var context = this.MakeDbContext(options);
                    await setup(context).ConfigureAwait(false);
                }

                using (var context = this.MakeDbContext(options))
                {
                    await runner(context).ConfigureAwait(false);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Makes the database context.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>TDbContext.</returns>
        private TDbContext MakeDbContext(DbContextOptions<TDbContext> options) => (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
    }
}
