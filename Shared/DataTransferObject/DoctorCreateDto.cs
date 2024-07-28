using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record DoctorCreateDto(string Name, decimal FeeForNewPatient, decimal FeeForOldPatient, string Speciality, string ScheduledDay, string ContactNo);

}
