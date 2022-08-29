using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Service.Utilities
{
    public class AppIdentityException : Exception
    {
        public AppIdentityException(string message) : base(message){}
    }
}