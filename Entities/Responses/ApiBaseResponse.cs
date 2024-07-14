using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public abstract class ApiBaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        protected ApiBaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
