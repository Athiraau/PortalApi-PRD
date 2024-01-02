using DataAccess.Dto.Request;
using FluentValidation;

namespace Portal.Validators
{
    public class GetAlertValidator : AbstractValidator<PortalDetailReqDto>
    {
        public GetAlertValidator() 
        {
            RuleFor(d => d.flag).NotNull().NotEmpty().WithMessage("Flag value should be a valid string");
        }
    }
}
