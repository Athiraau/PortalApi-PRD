using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class ServiceHelper
    {
        private ValidationHelper _pHelper;
        private HelperClass _helper;        

        private readonly IRepositoryWrapper _repository;
        private readonly IConfiguration _config;
        private readonly ErrorResponse _error;

        public ServiceHelper(IRepositoryWrapper repository, ErrorResponse error, IConfiguration config)
        {
            _repository = repository;
            _error = error;
            _config = config;
        }

        public ValidationHelper VHelper
        {
            get
            {
                if (_pHelper == null)
                {
                    _pHelper = new ValidationHelper(_repository, _error);
                }
                return _pHelper;
            }
        }

        public HelperClass Helper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new HelperClass(_config);
                }
                return _helper;
            }
        }
    }
}
