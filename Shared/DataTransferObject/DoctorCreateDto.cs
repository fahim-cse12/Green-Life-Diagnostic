namespace Shared.DataTransferObject
{
    public record DoctorCreateDto(string Name, decimal FeeForNewPatient, decimal FeeForOldPatient, string Speciality, string ScheduledDay, string ContactNo);

}
