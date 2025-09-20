using Contracts;
using Entities.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.NonDbEntities;

namespace Service;
public class DashboardService (IRepositoryManager repository) : IDashboardService
{
    public async Task<ApiBaseResponse> GetDashboardDataAsync(DateTime? startDate, DateTime? endDate)
    {
        string sp = DatabaseProcedure.DashboardQuery;   


        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@startDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value),
            new SqlParameter("@endDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value)
        };

        var resultList = await repository.ExecuteStoredProcedureToGetData<DashboardData>(sp, parameters).ToListAsync();
        var result = resultList.FirstOrDefault();

        return result == null
            ? new ApiOkResponse<DashboardData>(null, "Data not found")
            : new ApiOkResponse<DashboardData>(result, "Dashboard data retrieved successfully");
    }

}
