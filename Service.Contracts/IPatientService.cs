using Entities.Responses;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPatientService
    {
        Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchageTicket);
    }
}
