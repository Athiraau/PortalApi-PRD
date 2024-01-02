using DataAccess.Dto.Request;
using FluentValidation;

namespace Portal.Validators
{
    public class EmpVerifyOTPValidator : AbstractValidator<VerifyOTPReqDto>
    {
        public EmpVerifyOTPValidator() 
        {
            RuleFor(d => d.empId).NotNull().NotEmpty().WithMessage("Employee ID is required");
            RuleFor(d => Convert.ToString(d.empId)).Must(EmployeeIdLength).WithMessage("Employee ID Length must be equal 6");
            RuleFor(d => d.OTP).NotNull().NotEmpty().WithMessage("OTP is required");
            RuleFor(d => d.OTP).Must(OTPLength).WithMessage("OTP Length must be equal 6");
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

        private bool OTPLength(string otp)
        {
            if (otp.Length != 6)
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
