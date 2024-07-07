using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class DoctorNotFoundResponse : ApiNotFoundResponse
    {
        public DoctorNotFoundResponse(Guid id) : base($"Doctor with id: {id} is not found in db.")
        { }
    }
}
