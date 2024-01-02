using DataAccess.Dto.Request;
using DataAccess.Dto.Response;

namespace Business.Contracts
{
    public interface ILoginService
    {
        public Task<EmpOTPResDto> EmployeeSendOTP(EmpOTPReqDto smsReq);
        public Task<VerifyOTPResDto> EmployeeVerifyOTP(VerifyOTPReqDto smsReq);
        public Task<MessageResDto> ChangeEmpPassword(ChangePasswordReqDto pwdReq);
    }
}
