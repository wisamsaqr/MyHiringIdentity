using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Service.Utilities
{
    public class Pagination<T>
    {
        public T Data { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
    }
}