using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class ApiErrorResponse : ApiBaseResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiErrorResponse(string message, IEnumerable<string> errors) : base(false, message)
        {
            Errors = errors;
        }

        public ApiErrorResponse(string message, string error) : base(false, message)
        {
            Errors = new List<string> { error };
        }
    }
}
