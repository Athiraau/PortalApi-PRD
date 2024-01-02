using DataAccess.Contracts;
using Business.Contracts;
using Microsoft.Extensions.Configuration;
using DataAccess.Dto;
using Microsoft.AspNetCore.Hosting;

namespace Business.Services
{
    public class ServiceWrapper: IServiceWrapper
    {
        private IPortalService _portal;
        private ILoginService _login;
        private IJwtUtils _jwtUtil;
        private readonly IRepositoryWrapper _repository;        
        private IConfiguration _config;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _env;

        private readonly ServiceHelper _service;
        private readonly DtoWrapper _dto;

        public ServiceWrapper(IRepositoryWrapper repository, ServiceHelper service, IConfiguration config, 
            ILoggerService logger, DtoWrapper dto, IWebHostEnvironment env)
        {
            _repository = repository;
            _service = service;
            _config = config;
            _logger = logger;
            _dto = dto;
            _env = env;
        }


        public IPortalService Portal
        {
            get
            {
                if (_portal == null)
                {
                    _portal = new PortalService(_repository, _service);
                }
                return _portal;
            }
        }

        public ILoginService Login
        {
            get
            {
                if (_login == null)
                {
                    _login = new LoginService(_service, _dto, _config, _repository, _env);
                }
                return _login;
            }
        }

        public IJwtUtils JwtUtil
        {
            get
            {
                if (_jwtUtil == null)
                {
                    _jwtUtil = new JwtUtils(_config, _logger);
                }
                return _jwtUtil;
            }
        }
    }
}
