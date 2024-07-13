using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public sealed class ApiOkResponse<TResult> : ApiBaseResponse
    {
        public TResult Result { get; set; }

        public ApiOkResponse(TResult result, string message = "Request was successful")
            : base(true, message)
        {
            Result = result;
        }

    }
}
