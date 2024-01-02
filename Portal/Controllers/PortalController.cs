using Microsoft.AspNetCore.Mvc;
using DataAccess.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Business.Contracts;
using Newtonsoft.Json;
using DataAccess.Dto.Request;
using FluentValidation;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using DataAccess.Dto;
using Microsoft.VisualBasic;

namespace Portal.Controllers
{
    [Authorize]
   // [AllowAnonymous]
    [Route("api/portal")]
    [EnableCors("MyPolicy")]    
    [ApiController]
	public class PortalController : ControllerBase
	{
		private readonly ILoggerService _logger;
		private readonly IServiceWrapper _service;
        private readonly ServiceHelper _helper;
        private readonly DtoWrapper _dto;
        private readonly IValidator<PortalDetailReqDto> _pdVlaidator;
        private readonly IValidator<PortalInsertReqDto> _piValidator;
        private readonly IValidator<PortalValidateReqDto> _pvdValidator;
        private readonly IValidator<PortalDetailReqDto> _alertValidator;

        public PortalController(ILoggerService logger, IServiceWrapper service, IValidator<PortalDetailReqDto> pdVlaidator, 
            IValidator<PortalInsertReqDto> piValidator, IValidator<PortalValidateReqDto> pvdValidator, ServiceHelper helper,
            DtoWrapper dto, IValidator<PortalDetailReqDto> alertValidator)
		{
			_logger = logger;
			_service = service;
            _helper = helper;
            _dto = dto;
            _pdVlaidator = pdVlaidator;
            _piValidator = piValidator;
            _pvdValidator = pvdValidator;
            _alertValidator = alertValidator;
		}


        [HttpGet("GetPortalDetails/{flag}/{firmId}/{branchId}/{userId}/{sessionId}/{macId}/{fromDate}/{toDate}", Name = "GetPortalDetails")]
        public async Task<IActionResult> GetBranchDetails([FromRoute] string flag, int firmId, int branchId, int userId, string sessionId, string macId, string fromDate, string toDate)
        {
            _dto.portalDetailsReq.flag = flag;
            _dto.portalDetailsReq.firmId = firmId;
            _dto.portalDetailsReq.branchId = branchId;
            _dto.portalDetailsReq.userId = userId;
            _dto.portalDetailsReq.sessionId = sessionId;
            _dto.portalDetailsReq.macId = macId;
            _dto.portalDetailsReq.fromDate = Convert.ToDateTime(fromDate);
            _dto.portalDetailsReq.toDate = Convert.ToDateTime(toDate);

            var validationResult = await _pdVlaidator.ValidateAsync(_dto.portalDetailsReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);
            if (errorRes.errorMessage.Count > 0)
            {
              //  _logger.LogError("Invalid portal details sent from client.");
                return BadRequest(errorRes);
            }

            var portal = await _service.Portal.GetPortalDetails(flag, firmId, branchId, userId, sessionId, macId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
            if (portal == null)
            {
              //  _logger.LogError($"Branch details with user id: {userId}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
             //   _logger.LogInfo($"Returned branch details successfully with user id: {userId}");
                return Ok(JsonConvert.SerializeObject(portal));
            }
        }

        [HttpPost("Upsert", Name = "InsertUpdateDetails")]
        public async Task<IActionResult> InsertPortalDetails([FromBody] PortalInsertReqDto insertReq)
        {
            var validationResult = await _piValidator.ValidateAsync(insertReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);

            if (insertReq is null)
            {
              //  _logger.LogError("Portal object sent from client is null.");
                return BadRequest("Portal object is null");
            }
            else if (errorRes.errorMessage.Count > 0)
            {
              //  _logger.LogError("Invalid request data sent from client.");
                return BadRequest(errorRes);
            }
            else
            {
                var portal = await _service.Portal.InsertPortalDetails(insertReq);
              //  _logger.LogInfo($"Portal details inserted successfully");
                return Ok(JsonConvert.SerializeObject(portal));
            }
        }

        [HttpPost("Validate", Name = "ValidatePortalDetails")]
        public async Task<IActionResult> PortalValidation([FromBody] PortalValidateReqDto validateReq)
        {
            var validationResult = await _pvdValidator.ValidateAsync(validateReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);

            if (validateReq is null)
            {
              //  _logger.LogError("Portal object sent from client is null.");
                return BadRequest("Portal object is null");
            }
            else if (errorRes.errorMessage.Count > 0)
            {
              //  _logger.LogError("Invalid request data sent from client.");
                return BadRequest(errorRes);
            }
            else
            {
                var portal = await _service.Portal.PortalValidation(validateReq);
             //   _logger.LogInfo($"Portal details validated successfully");
                return Ok(JsonConvert.SerializeObject(portal));
            }
        }

        [HttpGet("GetAlerts/{flag}", Name = "GetAlerts")]
        public async Task<IActionResult> GetAlerts([FromRoute] string flag)
        {
            _dto.portalDetailsReq.flag = flag;
            int firmId, branchId, userId;
            firmId = branchId = userId = 0;
            string sessionId, macId;
            sessionId = macId = string.Empty;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            var validationResult = await _pdVlaidator.ValidateAsync(_dto.portalDetailsReq);
            var errorRes = _helper.VHelper.ReturnErrorRes(validationResult);
            if (errorRes.errorMessage.Count > 0)
            {
             //   _logger.LogError("Invalid portal details sent from client.");
                return BadRequest(errorRes);
            }

            var alerts = await _service.Portal.GetPortalDetails(flag, firmId, branchId, userId, sessionId, macId, fromDate, toDate);
            if (alerts == null)
            {
             //   _logger.LogError($"Alerts for portal display haven't been found in db.");
                return NotFound();
            }
            else
            {
             //   _logger.LogInfo($"Returned alerts successfully");
                return Ok(JsonConvert.SerializeObject(alerts));
            }
        }


        [HttpGet("SessionValidation/{sessionID}", Name = "SessionValidation")]
        public async Task<IActionResult> SessionValidation([FromRoute] string sessionID)
        {
            var res = await _service.Portal.SessionValidation(sessionID);
            if (res == null)
            {
                //   _logger.LogError($"Alerts for portal display haven't been found in db.");
                return NotFound();
            }
            else
            {

                //   _logger.LogInfo($"Returned alerts successfully");
                return Ok(JsonConvert.SerializeObject(res));
            }
        }
    }
}
