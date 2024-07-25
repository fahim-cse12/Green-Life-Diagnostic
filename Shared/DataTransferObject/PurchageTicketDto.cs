using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class PurchageTicketDto
    {
        public TicketDto ticketDto {get; set;}
        public  PatientDto patientDto {get; set;}
    }
}
