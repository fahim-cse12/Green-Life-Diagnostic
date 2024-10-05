using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utility
{
    public static class DatabaseProcedure
    {
        public static readonly string PatientSearchByQuery = "SP_SearchPatient";
        public static readonly string TicketDeleteQuery = "SP_DeletePurchasedTicket";
    }
}
