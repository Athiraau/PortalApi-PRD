using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Entities;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IPortalRepository
	{
		public Task<dynamic> GetPortalDetails(string flag, int firmId, int branchId, int userId, string sessionId, string param, string macId, DateTime fromDate, DateTime toDate);
		public Task<PortalInsertResDto> InsertPortalDetails(PortalInsertReqDto insertReq);
		public Task<PortalValidateResDto> PortalValidation(PortalValidateReqDto validateReq);
		public Task<dynamic> SessionValidation(string session);

    }
}
