using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Service.Utilities
{
    public class DataPagination<T>
    {
        public T Data { get; set; }
        public Paging Pagination { get; set; }
        public DataPagination(T data, int pageNumber, int PagesCount)
        {
            Data = data;
            Pagination = new Paging()
            {
                CurrentPage = pageNumber,
                PagesCount = PagesCount
            };
        }


        public class Paging
        {
            public int PagesCount { get; set; }
            public int CurrentPage { get; set; }
        }
    }
}