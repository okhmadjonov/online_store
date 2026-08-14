using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Application.Paginators
{
    public class PaginatedList
    {
        public const int MaxPageSize = 100;
    }

    public class PaginatedList<T> : PaginatedList
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();


    }
}
