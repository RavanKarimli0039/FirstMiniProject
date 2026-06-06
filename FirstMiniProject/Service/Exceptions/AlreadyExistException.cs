using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message) : base(message)
        {
            
        }
    }
}
