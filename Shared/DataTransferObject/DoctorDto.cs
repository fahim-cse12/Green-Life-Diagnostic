namespace Shared.DataTransferObject
{
    public record DoctorDto : BaseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal FeeForNewPatient { get; set; }
        public decimal FeeForOldPatient { get; set; }
        public string Speciality { get; init; }
        public string ScheduledDay { get; init; }
        public string ContactNo { get; init; }
    }
}
