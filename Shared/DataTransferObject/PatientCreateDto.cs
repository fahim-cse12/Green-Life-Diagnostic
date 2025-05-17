namespace Shared.DataTransferObject
{
    public record PatientCreateDto(string Name, string Gender, string Mobile, int Age, string Address, int PatientType);
   
}
