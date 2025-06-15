using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IPatientService
    {
        Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchageTicket);
        Task<ApiBaseResponse> PatientSearchByQuery(string? ticketId, string? patientName, string? mobileNo, string? doctorName, DateTime? startDate, DateTime? endDate);
        Task<ApiBaseResponse> DeletePurchasedTicketAsync(Guid ticketId, bool trackChanges);
    }
}
