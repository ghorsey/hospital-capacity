namespace Gah.HC.Repository
{
    using System;

    /// <summary>
    /// Interface ITransaction
    /// Implements the <see cref="System.IDisposable" />.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        Guid TransactionId { get; }

        /// <summary>
        /// Commits this Transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this transaction.
        /// </summary>
        void Rollback();
    }
}
