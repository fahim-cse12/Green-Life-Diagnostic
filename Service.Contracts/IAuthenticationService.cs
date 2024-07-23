using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiBaseResponse> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
       // Task<TokenDto> CreateToken(bool populateExp);
        //Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
