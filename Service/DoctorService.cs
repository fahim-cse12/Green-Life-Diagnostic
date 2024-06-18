using AutoMapper;
using Contracts;
using LoggerService;
using Service.Contracts;

namespace Service
{
    internal sealed class DoctorService : IDoctorService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public DoctorService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


    }
}
