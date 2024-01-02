using DataAccess.Dto.Request;
using FluentValidation;
using System;

namespace Portal.Validators
{
    public class PortalDetailsValidator : AbstractValidator<PortalDetailReqDto>
    {
        public PortalDetailsValidator()
        {
            RuleFor(d => d.flag).NotNull().NotEmpty().WithMessage("Flag value should be a valid string");
            RuleFor(d => d.firmId).NotNull().GreaterThan(0).WithMessage("FirmId must be greater than 0");
            RuleFor(d => d.branchId).NotNull().WithMessage("BranchId is mandatory");
            RuleFor(d => d.userId).Must(EmpcodeLength).WithMessage("UserId Length must be equal or less than 6");
            RuleFor(d => d.sessionId).NotNull().NotEmpty().WithMessage("SessionId should be a valid string");
            RuleFor(d => d.macId).NotNull().NotEmpty().WithMessage("MacId value should be a valid string");
            RuleFor(d => d.fromDate).NotNull().Must(BeAValidDate).WithMessage("From date should be a valid date value");
            RuleFor(d => d.toDate).NotNull().Must(BeAValidDate).WithMessage("From date should be a valid date value");
            RuleFor(d => d.fromDate).LessThanOrEqualTo(x => x.toDate).WithMessage("From date should be greater value than To date");
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
