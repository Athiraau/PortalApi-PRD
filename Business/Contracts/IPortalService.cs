using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IPortalService
    {
        public Task<dynamic> GetPortalDetails(string flag, int firmId, int branchId, int userId, string sessionId, string macId, DateTime fromDate, DateTime toDate);
        public Task<PortalInsertResDto> InsertPortalDetails(PortalInsertReqDto insertReq);
        public Task<PortalValidateResDto> PortalValidation(PortalValidateReqDto validateReq);
        public Task<dynamic> SessionValidation(string sessionId);
        public Task<dynamic> GetAlerts(string flag, int firmId, int branchId, int userId, string sessionId, string macId, DateTime fromDate, DateTime toDate);
    }
}
