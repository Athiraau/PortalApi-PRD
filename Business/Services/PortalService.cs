using Business.Contracts;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Entities;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PortalService : IPortalService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ServiceHelper _service;

        public PortalService(IRepositoryWrapper repository, ServiceHelper service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<dynamic> GetPortalDetails(string flag, int firmId, int branchId, int userId, string sessionId, string macId, DateTime fromDate, DateTime toDate)
        {
            var portal = await _repository.Portal.GetPortalDetails(flag, firmId, branchId, userId, sessionId, null, macId, fromDate, toDate);
            return portal;
        }

        public async Task<PortalInsertResDto> InsertPortalDetails(PortalInsertReqDto insertReq)
        {
            PortalInsertResDto portal = await _repository.Portal.InsertPortalDetails(insertReq);
            return portal;
        }        

        public async Task<PortalValidateResDto> PortalValidation(PortalValidateReqDto validateReq)
        {
            PortalValidateResDto portal = await _repository.Portal.PortalValidation(validateReq);
            return portal;
        }

        public async Task<dynamic> GetAlerts(string flag, int firmId, int branchId, int userId, string sessionId, string macId, DateTime fromDate, DateTime toDate)
        {
            var portal = await _repository.Portal.GetPortalDetails(flag, firmId, branchId, userId, sessionId, macId, macId, fromDate, toDate);
            return portal;
        }

        public async Task<dynamic> SessionValidation(string  sessionId)
        {
            var portal = await _repository.Portal.SessionValidation(sessionId);
          //  int count = Convert.ToInt32(portal);
            return portal;
        }
    }
}
