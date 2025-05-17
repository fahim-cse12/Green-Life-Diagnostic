namespace Entities.Responses
{
    public abstract class ApiNotFoundResponse : ApiBaseResponse
    {
        public ApiNotFoundResponse(string message) : base(false, message)
        {
        }
    }
}
