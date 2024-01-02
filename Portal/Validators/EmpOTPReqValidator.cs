using DataAccess.Dto.Request;
using FluentValidation;

namespace Portal.Validators
{
    public class EmpOTPReqValidator : AbstractValidator<EmpOTPReqDto>
    {
        public EmpOTPReqValidator()
        {
            RuleFor(d => d.empId).NotNull().NotEmpty().WithMessage("Employee ID is required");
            RuleFor(d => Convert.ToString(d.empId)).Must(EmployeeIdLength).WithMessage("Employee ID Length must be equal 6");
            RuleFor(d => d.mobileNo).NotNull().NotEmpty().WithMessage("Mobile number is required");
            RuleFor(d => d.mobileNo).Must(MobileLength).WithMessage("Mobile No Length must be equal 10 (without country code)");
            RuleFor(d => d.accId).NotNull().NotEmpty().GreaterThan(0).WithMessage("account id should always greater than ");            
        }

        private bool EmployeeIdLength(string emptId)
        {
            if (emptId.Length > 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool MobileLength(string mobileNo)
        {
            if (mobileNo.Length != 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
