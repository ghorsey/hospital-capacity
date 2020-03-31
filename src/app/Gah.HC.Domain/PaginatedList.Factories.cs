namespace Gah.HC.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// The factory methods for the <see cref="PaginatedList{T}"/> object.
    /// </summary>
    public static class PaginatedList
    {
        /// <summary>
        /// Makes the specified items.
        /// </summary>
        /// <typeparam name="T">The entity type of the items in the list.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="total">The total.</param>
        /// <param name="page">The page.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        /// A new <see cref="PaginatedList{T}" />.
        /// </returns>
        public static PaginatedList<T> MakePaginatedList<T>(this IList<T> items, int total, int page, int size)
            where T : class
        {
            return new PaginatedList<T>(items, total, page, size);
        }
    }
}
