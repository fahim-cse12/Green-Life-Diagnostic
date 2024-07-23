﻿using AutoMapper;
using Entities.ConfigurationModels;
using Entities.Models;
using Entities.Responses;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly JwtConfiguration _jwtConfiguration;

        private User? _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfiguration = _configuration.Value;
        }

        public async Task<ApiBaseResponse> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            else
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }
            return new ApiOkResponse<UserForRegistrationDto>(userForRegistration, "User created successfully");

        }

        public Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            throw new NotImplementedException();
        }
    }
}
