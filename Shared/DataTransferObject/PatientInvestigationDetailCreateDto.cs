namespace Shared.DataTransferObject
{
    public record PatientInvestigationDetailCreateDto
    (
        Guid InvestigationId,
        decimal PaymentAmount
    );
}
