using DataAccess.Dto.Request;
using FluentValidation;

namespace Portal.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordReqDto>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(d => d.empCode).NotNull().GreaterThan(0).WithMessage("EmpID must be greater than 0");            
            RuleFor(d => d.empCode).Must(EmpcodeLength).WithMessage("Employee Code Length must be equal or less than 6");
            RuleFor(d => d.password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(d => Convert.ToDateTime(d.joingDate)).NotNull().Must(BeAValidDate).WithMessage("Joining date should be a valid date value");
            RuleFor(d => Convert.ToDateTime(d.dateOfBirth)).NotNull().Must(BeAValidDate).WithMessage("Date of birth should be a valid date value");
        }

        private bool EmpcodeLength(int empCode)
        {
            if (Convert.ToString(empCode).Length > 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
