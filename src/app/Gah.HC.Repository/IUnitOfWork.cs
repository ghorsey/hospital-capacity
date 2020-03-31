namespace Gah.HC.Repository
{
    using System;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits the changes in this unit of work asynchronously.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Commits the changes in this unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Executes the in resilient transaction asynchronously.
        /// Return <c>false</c> to rollback the transaction (or throw an exception).
        /// </summary>
        /// <param name="actionAsync">The asynchronous action.</param>
        /// <returns>Task.</returns>
        Task ExecuteInResilientTransactionAsync(Func<Task<bool>> actionAsync);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>ITransaction.</returns>
        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>ITransaction.</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// Begins the transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ITransaction&gt;.</returns>
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins the transaction asynchronously.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ITransaction&gt;.</returns>
        Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    }
}
