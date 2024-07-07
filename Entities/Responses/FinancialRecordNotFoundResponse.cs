using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class FinancialRecordNotFoundResponse : ApiNotFoundResponse
    {
        public FinancialRecordNotFoundResponse(Guid id) : base($"Financial Record with id: {id} is not found in db.")
        { }
    }
}
