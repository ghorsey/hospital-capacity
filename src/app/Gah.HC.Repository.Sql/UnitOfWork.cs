namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class UnitOfWork.
    /// Implements the <see cref="IUnitOfWork" />.</summary>
    /// <typeparam name="TDbContext">The type of the t database context.</typeparam>
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork<TDbContext> : IUnitOfWork
            where TDbContext : DbContext
    {
        private readonly ILogger logger;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public UnitOfWork(TDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger<UnitOfWork<TDbContext>>();
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        protected TDbContext DbContext { get; }

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        /// <value>The logger factory.</value>
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public Task<int> CommitAsync()
        {
            this.logger.LogTrace($"Calling {nameof(this.CommitAsync)}");
            return this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            this.logger.LogTrace($"Calling {nameof(this.Commit)}");
            this.DbContext.SaveChanges();
        }

        /// <summary>
        /// Executes the in resilient transaction asynchronous.
        /// Return <c>false</c> to rollback the transaction (or throw an exception).
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Task.</returns>
        /// <remarks>if the action returns false, the transaction will be rolled back.</remarks>
        public Task ExecuteInResilientTransactionAsync(Func<Task<bool>> action)
        {
            this.logger.LogTrace($"Calling {nameof(this.ExecuteInResilientTransactionAsync)}.");
            var strategy = this.DbContext.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                using var transaction = this.DbContext.Database.BeginTransaction();
                try
                {
                    var shouldCommit = await action().ConfigureAwait(false);
                    if (shouldCommit)
                    {
                        this.logger.LogInformation("Commiting Transaction");
                        transaction.Commit();
                    }
                    else
                    {
                        this.logger.LogInformation("Rolling back transaction");
                        transaction.Rollback();
                    }
                }
                catch (Exception x)
                {
                    this.logger.LogError(x, "Failed running in transaction {message}", x.Message);
                    transaction.Rollback();
                    throw;
                }
            });
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>ITransaction.</returns>
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            this.logger.LogTrace($"Calling {nameof(this.BeginTransaction)} with the isolation level {isolationLevel}", isolationLevel);
            return new TransactionWrapper(this.DbContext.Database.BeginTransaction(isolationLevel));
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>ITransaction.</returns>
        public ITransaction BeginTransaction()
        {
            this.logger.LogTrace($"Calling {nameof(this.BeginTransaction)}");
            return new TransactionWrapper(this.DbContext.Database.BeginTransaction());
        }

        /// <summary>
        /// Begin transaction as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ITransaction&gt;.</returns>
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace($"Calling {nameof(this.BeginTransactionAsync)}");
            var t = await this.DbContext.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            return new TransactionWrapper(t);
        }

        /// <summary>
        /// Begins the transaction asynchronously.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ITransaction&gt;.</returns>
        public async Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace($"Calling {nameof(this.BeginTransactionAsync)} with the isolation level {isolationLevel}", isolationLevel);
            var t = await this.DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
            return new TransactionWrapper(t);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.logger.LogTrace($"{nameof(this.Dispose)} called with a disposing value of {disposing}");
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.DbContext.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
