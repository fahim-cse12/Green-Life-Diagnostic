using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IPatientService
    {
        Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchageTicket);
        Task<ApiBaseResponse> PatientSearchByQuery(string? ticketId, string? patientName, string? mobileNo, string? doctorName, DateOnly? startDate, DateOnly? endDate);
        Task<ApiBaseResponse> DeletePurchasedTicketAsync(Guid ticketId, bool trackChanges);
    }
}
