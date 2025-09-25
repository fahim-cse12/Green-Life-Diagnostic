using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Responses;

namespace Service.Contracts;
public interface IDashboardService
{
    Task<ApiBaseResponse> GetDashboardDataAsync(DateOnly? startDate, DateOnly? endDate);
}
