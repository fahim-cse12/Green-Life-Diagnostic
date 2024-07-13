using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public abstract class ApiNotFoundResponse : ApiBaseResponse
    {
        public ApiNotFoundResponse(string message) : base(false, message)
        {
        }
    }
}
