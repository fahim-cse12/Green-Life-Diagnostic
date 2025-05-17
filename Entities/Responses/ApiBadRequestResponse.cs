namespace Entities.Responses
{
    public class ApiBadRequestResponse : ApiBaseResponse
    {
        public ApiBadRequestResponse(string message) : base(false, message)
        {
        }
    }
}
