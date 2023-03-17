using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Constants;

public interface IPagedList
{
    int PageCount { get; }
    int TotalItemCount { get; }

    int PageIndex { get; }
    int PageNumber { get; }
    int PageSize { get; }
    int HasPreviousPage { get; }

    int HasNextPage { get; }
    int IsFirstPage { get; }

    int IsLastPage { get; }
    int FirstItemIndex { get; }
    int LastItemIndex { get; }

    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
        T this[int index] { get; }

        int Count { get; }
    }

}
