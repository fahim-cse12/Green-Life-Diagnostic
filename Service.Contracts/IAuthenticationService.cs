using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiBaseResponse> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);

        Task<ApiBaseResponse> ChangePasswordAsync(
            Guid userId,
            ChangePasswordRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
