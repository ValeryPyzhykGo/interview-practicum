using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;

namespace Application.Exceptions
{
    internal class ForbidenMultipleMenuPostionException : ApplicationException
    {
        public ForbidenMultipleMenuPostionException(string dishName) : base($"Multiple {dishName}(s) not allowed")
        {
        }
    }
}
