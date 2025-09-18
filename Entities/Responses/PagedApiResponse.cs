using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses;
public class PagedApiResponse<T>(
    T result,
    int totalRecords,
    string message = "")
    : ApiBaseResponse(true, message)
{
    public T Result { get; } = result;
    public int TotalRecords { get; } = totalRecords;
}
