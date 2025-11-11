using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Exceptions
{
    public class InvalidDateException : ApplicationException
    {
        public readonly DateTime _invalidDate;
        public InvalidDateException(DateTime invalidDate) : base($"The Date provided {invalidDate.Date} is invalid for this Operation")
        {
            _invalidDate = invalidDate;
        }
    }
}