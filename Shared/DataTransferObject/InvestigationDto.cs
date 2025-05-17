namespace Shared.DataTransferObject
{
    public record InvestigationDto : BaseDto
    {
        public Guid Id { get; init; }
        public string InvestigationName { get; init; }
        public decimal Cost { get; init; }
        public string Description { get; init; }
    }
}
