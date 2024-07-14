using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class ApiBadRequestResponse : ApiBaseResponse
    {
        public ApiBadRequestResponse(string message) : base(false, message)
        {
        }
    }
}
