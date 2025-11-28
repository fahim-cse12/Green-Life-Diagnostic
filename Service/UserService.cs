using AutoMapper;
using Contracts;
using Entities.Responses;
using LoggerService;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        //public Task<ApiBaseResponse> CreateUserAsync(UserForRegistrationDto userDto)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ApiBaseResponse> DeleteUserAsync(Guid userId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiBaseResponse> GetAllUserAsync(bool trackChanges)
        {
            var userList = await _repository.User.GetAllUserAsync(trackChanges);

            var userDto = _mapper.Map<IEnumerable<UserForRegistrationDto>>(userList);

            return new ApiOkResponse<IEnumerable<UserForRegistrationDto>>(userDto, "");
        }

        public async Task<ApiBaseResponse> GetUserAsync(Guid userId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiBaseResponse> UpdateUserAsync(Guid userId, UserForRegistrationDto userDto, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
