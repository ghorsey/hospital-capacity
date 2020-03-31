namespace Gah.HC.Repository.Sql
{
    using System;
    using Microsoft.EntityFrameworkCore.Storage;

    /// <summary>
    /// Class TransactionWrapper.
    /// Implements the <see cref="ITransaction" />.
    /// </summary>
    /// <seealso cref="ITransaction" />
    internal sealed class TransactionWrapper : ITransaction
    {
        private readonly IDbContextTransaction dbContextTransaction;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWrapper"/> class.
        /// </summary>
        /// <param name="dbContextTransaction">The database context transaction.</param>
        public TransactionWrapper(IDbContextTransaction dbContextTransaction)
        {
            this.dbContextTransaction = dbContextTransaction;
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public Guid TransactionId => this.dbContextTransaction.TransactionId;

        /// <summary>
        /// Commits this transaction.
        /// </summary>
        public void Commit() => this.dbContextTransaction.Commit();

        /// <summary>
        /// Rollbacks this transaction.
        /// </summary>
        public void Rollback() => this.dbContextTransaction.Rollback();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContextTransaction.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
