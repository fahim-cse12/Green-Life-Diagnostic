namespace Shared.DataTransferObject
{
    public record PatientInvestigationDetailUpdateDto
    (
        Guid PatientInvestigationDetailId,
        Guid PatientInvestigationId,
        Guid InvestigationId,
        decimal PaymentAmount,
        string? ResultText,
        string? ResultDate
    );
}
