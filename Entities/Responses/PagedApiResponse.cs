using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses;
public class PagedApiResponse<T>(
    T data,
    int totalRecords,
    int pageNumber,
    int pageSize,
    string message = "")
    : ApiBaseResponse(true, message)
{
    public T Data { get; } = data;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalRecords { get; } = totalRecords;
    public int TotalPages { get; } = (int)Math.Ceiling(totalRecords / (double)pageSize);
}
