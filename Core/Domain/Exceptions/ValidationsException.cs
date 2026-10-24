using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationsException : Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];

        public ValidationsException(IEnumerable<string> errors)
            : base("Validation failed")
        {
            Errors = errors;
        }
    }
}
