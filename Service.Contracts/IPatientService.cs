using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IPatientService
    {
        Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchageTicket);
        Task<ApiBaseResponse> PatientSearchByQuery(string ticketId, string patientName = null, string mobileNo = null, string doctorName = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<ApiBaseResponse> DeletePurchasedTicketAsync(Guid ticketId, bool trackChanges);
    }
}
