using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.ViewModels
{
    public class OperationResult<T>
    {
        public bool Status { get; set; }
        public T Id { get; set; }
        public string Message { get; set; }
    }
}