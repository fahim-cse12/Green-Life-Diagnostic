namespace Entities.Responses
{
    public sealed class ApiOkResponse<TResult> : ApiBaseResponse
    {
        public TResult Result { get; set; }

        public ApiOkResponse(TResult result, string message = "")
            : base(true, message)
        {
            Result = result;
        }

    }
}
