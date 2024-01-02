using DataAccess.Contracts;
using DataAccess.Entities;
using System.Collections.Generic;

namespace Business.Helpers
{
    public class ValidationHelper
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ErrorResponse _error;

        public ValidationHelper(IRepositoryWrapper repository, ErrorResponse error)
        {
            _repository = repository;
            _error = error;
        }

        public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)
        {
            List<string> errors = new List<string>();
            foreach (var row in Res.Errors.ToArray())
            {
                errors.Add(row.ErrorMessage.ToString());
            }
            _error.errorMessage = errors;
            return _error;
        }
    }
}
