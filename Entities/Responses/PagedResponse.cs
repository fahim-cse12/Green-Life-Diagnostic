namespace Entities.Responses
{
    public class PagedResponse<T>
    {
        public T Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }

        public PagedResponse(T result, int pageNumber, int pageSize, int totalRecords)
        {
            Result = result;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        }
    }
}
