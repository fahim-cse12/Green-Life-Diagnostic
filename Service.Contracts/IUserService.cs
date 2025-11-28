using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<ApiBaseResponse> GetAllUserAsync(bool trackChanges);
        Task<ApiBaseResponse> GetUserAsync(Guid userId, bool trackChanges);
        //Task<ApiBaseResponse> CreateUserAsync(UserForRegistrationDto userDto);
        Task<ApiBaseResponse> DeleteUserAsync(Guid userId, bool trackChanges);
        Task<ApiBaseResponse> UpdateUserAsync(Guid userId, UserForRegistrationDto userDto, bool trackChanges);

    }
}
