using Entities.Responses;

namespace GreenLife.Presentation.Extentions
{
    public static class ApiBaseResponseExtentions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) => ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
