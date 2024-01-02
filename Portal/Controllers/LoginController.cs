using Business.Contracts;
using Business.Services;
using DataAccess.Contracts;
using DataAccess.Dto.Request;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Portal.Controllers
{
    
    [AllowAnonymous]
    [Route("api/login")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly ServiceHelper _helper;
        private readonly IValidator<EmpOTPReqDto> _empOTPValidator;
        private readonly IValidator<VerifyOTPReqDto> _verifyOTPValidator;
        private readonly IValidator<ChangePasswordReqDto> _changePwdValidator;

        public LoginController(IServiceWrapper service, ILoggerService logger, IValidator<EmpOTPReqDto> empOTPValidator,
            ServiceHelper helper, IValidator<VerifyOTPReqDto> verifyOTPValidator, IValidator<ChangePasswordReqDto> changePwdValidator)
        {
            _service = service;
            _logger = logger;
            _empOTPValidator = empOTPValidator;
            _helper = helper;
            _verifyOTPValidator = verifyOTPValidator;
            _changePwdValidator = changePwdValidator;
        }

        [HttpPost("EmployeeSendOTP", Name = "EmployeeSendOTP")]
        public async Task<IActionResult> EmployeeSendOTP([FromBody] EmpOTPReqDto smsReq)
        {
            var validationResult = await _empOTPValidator.ValidateAsync(smsReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);

            if (errorRes.errorMessage.Count > 0)
            {
             //   _logger.LogError("Invalid details sent from client.");
                return BadRequest(errorRes);
            }

            var smsStatus = await _service.Login.EmployeeSendOTP(smsReq);
            if (smsStatus == null)
            {
             //   _logger.LogError($"Details of SMS sent to employee hasn't been found in db.");
                return NotFound();
            }
            else
            {
              //  _logger.LogInfo($"Returned details of SMS sent.");
                return Ok(JsonConvert.SerializeObject(smsStatus));
            }
        }

        [HttpPost("EmployeeVerifyOTP", Name = "EmployeeVerifyOTP")]
        public async Task<IActionResult> EmployeeVerifyOTP([FromBody] VerifyOTPReqDto otpReq)
        {
            var validationResult = await _verifyOTPValidator.ValidateAsync(otpReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);

            if (errorRes.errorMessage.Count > 0)
            {
              //  _logger.LogError("Invalid details sent from client.");
                return BadRequest(errorRes);
            }

            var Status = await _service.Login.EmployeeVerifyOTP(otpReq);
            if (Status == null)
            {
              //  _logger.LogError($"Details of verifying OTP of employee hasn't been found in db.");
                return NotFound();
            }
            else
            {
              //  _logger.LogInfo($"Returned details of OTP verification.");
                return Ok(JsonConvert.SerializeObject(Status));
            }
        }

        [HttpPost("ChangeEmpPassword", Name = "ChangeEmpPassword")]
        public async Task<IActionResult> ChangeEmpPassword([FromBody] ChangePasswordReqDto pwdReq)
        {
            var validationResult = await _changePwdValidator.ValidateAsync(pwdReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);

            if (errorRes.errorMessage.Count > 0)
            {
              //  _logger.LogError("Invalid details sent from client.");
                return BadRequest(errorRes);
            }

            var Status = await _service.Login.ChangeEmpPassword(pwdReq);
            if (Status == null)
            {
              //  _logger.LogError($"No data found related to password change.");
                return NotFound();
            }
            else
            {
              //  _logger.LogInfo($"Returned details of password change.");
                return Ok(JsonConvert.SerializeObject(Status));
            }
        }
    }
}
