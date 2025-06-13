namespace Shared.DataTransferObject
{
    public record PatientDto : BaseDto
    {
        public Guid Id { get; init; }
        public string PatientUniqueId { get; set; }
        public string Name { get; init; }
        public string Gender { get; init; }
        public string Mobile { get; init; }
        public int Age { get; init; }
        public string Address { get; init; }
        public bool IsNewPatient { get; init; } // "Old" or "New"
    }
}
