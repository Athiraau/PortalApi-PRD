using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static System.Net.WebRequestMethods;

namespace Business.Services
{
    public class LoginService: ILoginService
    {

        private readonly ServiceHelper _helper;
        private readonly DtoWrapper _dto;
        private readonly IConfiguration _config;
        private readonly IRepositoryWrapper _repo;
        private readonly IHostEnvironment _env;
        public LoginService(ServiceHelper helper, DtoWrapper dto, IConfiguration config, IRepositoryWrapper repo,
            IHostEnvironment env)
        {
            _helper = helper; 
            _dto = dto;
            _config = config;
            _repo = repo;
            _env = env;
        } 

        public async Task<EmpOTPResDto> EmployeeSendOTP(EmpOTPReqDto smsReq)
        {
            //Verify customer mobile number
            var cellNo = await _repo.Portal.GetPortalDetails("PWDRESET", (int)PasswordChange.ValidateEmpMobile, 0, smsReq.empId, string.Empty, string.Empty, smsReq.mobileNo, DateTime.MinValue, DateTime.MinValue);
            if(cellNo.ISVALIDMOB == 0)
            {
                _dto.EmpOTPResponse.status = (int)SaveOTPStatus.NoDataFound;
                _dto.EmpOTPResponse.message = "Entered cell number is not associated with the employee";
            }
            else
            {
                //Generate OTP
                var otpLength = Convert.ToInt32(_config["OTPLength"]);
                var OTP = _helper.Helper.GenerateOTP(otpLength);

                //Create SendSMS request
                string mobileNo = smsReq.mobileNo;
                string SMSContent = "", smsResult = "";
                SMSContent = "Dear Customer,Your OTP for Manappuram eService portal is " + OTP + ". (Usable only once and valid for 5 mins.) DO NOT SHARE WITH ANY ONE. Manappuram";
                //SMSContent = smsReq.message;
                smsResult = _helper.Helper.SolutionInfiniSend(smsReq.accId, mobileNo, SMSContent);

                if(smsResult != null)
                {
                    //Save OTP in database
                    var res = await _repo.Portal.GetPortalDetails("PWDRESET", (int)PasswordChange.SaveOTP, 0, smsReq.empId, string.Empty, string.Empty, OTP, System.DateTime.MinValue, System.DateTime.MinValue);
                    if (res != null)
                    {
                        var otpLife = await _repo.Portal.GetPortalDetails("PWDRESET", (int)PasswordChange.GetOTPLife, 0, smsReq.empId, string.Empty, string.Empty, OTP, System.DateTime.MinValue, System.DateTime.MinValue);
                        _dto.EmpOTPResponse.status = (int)SaveOTPStatus.Success;
                        _dto.EmpOTPResponse.OTPLife = (int)otpLife.OTP_LIFE;
                        _dto.EmpOTPResponse.message = "SMS sent successfully";
                    }
                    else
                    {
                        _dto.EmpOTPResponse.status = (int)SaveOTPStatus.NoDataFound;
                        _dto.EmpOTPResponse.message = "Save OTP to corresponding employee failed";
                    }
                }
                else
                {
                    _dto.EmpOTPResponse.status = (int)SaveOTPStatus.NoDataFound;
                    _dto.EmpOTPResponse.message = "Unsuccessful attempt";
                }                
            }
            return _dto.EmpOTPResponse;
        }

        public async Task<VerifyOTPResDto> EmployeeVerifyOTP(VerifyOTPReqDto otpReq)
        {
            //Verify OTP
            var result = await _repo.Portal.GetPortalDetails("PWDRESET", (int)PasswordChange.VerifyOTP, 0, otpReq.empId, string.Empty, string.Empty, otpReq.OTP, System.DateTime.MinValue, System.DateTime.MinValue);
            if (result != null && (int)result.ISVALIDOTP == 1)
            {
                _dto.VerifyOTPRes.status = (int)result.ISVALIDOTP;
                _dto.VerifyOTPRes.message = "OTP verified successfully";
            }
            else
            {
                _dto.VerifyOTPRes.status = (int)result.ISVALIDOTP;
                _dto.VerifyOTPRes.message = "OTP verification failed";
            }

            return _dto.VerifyOTPRes;
        }

        public async Task<MessageResDto> ChangeEmpPassword(ChangePasswordReqDto pwdReq)
        {
            //Verify Employee
            var isValidEmp = await _repo.Portal.GetPortalDetails("PWDRESET", (int)PasswordChange.ValidateEmployee, 0, pwdReq.empCode, string.Empty, string.Empty, string.Empty, Convert.ToDateTime(pwdReq.dateOfBirth), Convert.ToDateTime(pwdReq.joingDate));
            if(isValidEmp == null || isValidEmp.ISVALID == 0)
            {
                _dto.MessageRes.message = "DOJ/DOB mismatch";
            }
            else          
            {
                var oldPwd = await _repo.Login.GetEmployeePassword(pwdReq.empCode);
                if (oldPwd == null)
                {
                    _dto.MessageRes.message = "Failed to get employee password";
                }
                else if (oldPwd == pwdReq.password)
                {
                    _dto.MessageRes.message = "Old and new password can not be same";
                }
                else
                {
                    MessageResDto result = null;
                    //change employee password
                    if (_env.IsProduction())
                    {
                        byte[] data = _helper.Helper.getEdata(Convert.ToString(pwdReq.empCode), pwdReq.password);
                        byte[] oldData = _helper.Helper.getEdata(Convert.ToString(pwdReq.empCode), oldPwd);
                        result = await _repo.Login.ChangePasswordProd(pwdReq.empCode, data, oldData);
                    }
                    else
                    {
                        result = await _repo.Login.ChangePassword(pwdReq, oldPwd);
                    }
                    if (result == null)
                    {
                        _dto.MessageRes.message = "Unable to change password";
                    }
                    else
                    {
                        if (result.message == "D")
                        {
                            _dto.MessageRes.message = result.message + ": Password successfully changed";
                        }
                        else
                        {
                            _dto.MessageRes.message = result.message + ": Password change unsuccessful";
                        }
                    }
                }
            }

            return _dto.MessageRes;
        }
    }
}
