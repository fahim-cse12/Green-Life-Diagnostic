namespace Entities.Responses
{
    public class IdNotFoundResponse<T> : ApiNotFoundResponse
    {
        public IdNotFoundResponse(Guid id) : base($"{typeof(T).Name} with id: {id} is not found in db.")
        { }
    }
}
