namespace Gah.HC.Domain
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    /// <summary>
    /// A paginated list.
    /// </summary>
    /// <typeparam name="T">The  type contained in the items list.</typeparam>
    [DebuggerDisplay("Total: {" + nameof(Total) + "}; Page: {" + nameof(Page) + "}; Size: {" + nameof(Size) + "}")]
    public class PaginatedList<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="total">The total.</param>
        /// <param name="page">The page.</param>
        /// <param name="size">The size.</param>
        public PaginatedList(IList<T> items, int total, int page, int size)
        {
            this.Items = new ReadOnlyCollection<T>(items);
            this.Page = page;
            this.Size = size;
            this.Total = total;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IReadOnlyList<T> Items { get;  }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get;  }
    }
}
