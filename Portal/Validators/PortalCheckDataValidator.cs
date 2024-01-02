using DataAccess.Dto.Request;
using FluentValidation;
using System;

namespace Portal.Validators
{
    public class PortalCheckDataValidator : AbstractValidator<PortalValidateReqDto>
    {
        public PortalCheckDataValidator()
        {
            RuleFor(d => d.macId).NotNull().NotEmpty().WithMessage("MacId value should be a valid string");
            RuleFor(d => d.hostName).NotNull().NotEmpty().WithMessage("Hostname should be a valid string");
            RuleFor(d => d.osInstallDate).NotNull().NotEmpty().WithMessage("OS Installation date should be a valid string");
            RuleFor(d => d.param).NotNull().NotEmpty().WithMessage("Param value should be a valid string");
            RuleFor(d => d.userId).Must(EmpcodeLength).WithMessage("UserId Length must be equal or less than 6");
            RuleFor(d => d.empPunchedBr).NotNull().WithMessage("BranchId is mandatory");
            RuleFor(d => d.roleId).NotNull().GreaterThan(0).WithMessage("RoleId must be greater than 0");                        
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
    }
}
