using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using System.Data;
using DataAccess.Dto.Response;
using DataAccess.Dto.Request;
using Oracle.ManagedDataAccess.Types;
using System.Dynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository
{
    public class PortalRepository : IPortalRepository
	{
		private readonly DapperContext _context;
		private readonly DtoWrapper _dto;

		public PortalRepository(DapperContext context, DtoWrapper dto)
		{
			_context = context;
			_dto = dto;
		}

		public async Task<dynamic> GetPortalDetails(string flag, int firmId, int branchId, int userId, string sessionId, string param, string macId, DateTime fromDate, DateTime toDate)
		{
			OracleRefCursor result = null;

			var procedureName = "PLP_Portal_Select";
			var parameters = new OracleDynamicParameters();
			parameters.Add("as_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("al_firmid", firmId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("al_bracnchid", branchId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("al_userid", userId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("as_sessionid", sessionId, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_param", param, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_macid", macId, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("ad_frmdate", fromDate, OracleMappingType.Date, ParameterDirection.Input);
			parameters.Add("ad_todate", toDate, OracleMappingType.Date, ParameterDirection.Input);
			parameters.Add("as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);

			parameters.BindByName = true;
			using var connection = _context.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<dynamic>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);

			return data;
		}

		public async Task<PortalInsertResDto> InsertPortalDetails(PortalInsertReqDto insertReq)
		{
			var result = string.Empty;

			var procedureName = "PLP_PORTAL_INSUPD";
			var parameters = new OracleDynamicParameters();
			parameters.Add("as_optflag", insertReq.optflag, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("an_firmid", insertReq.firmId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("an_branchid", insertReq.branchId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("as_mac_address", insertReq.macAddress, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_sessionid", insertReq.sessionId, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_param", insertReq.param, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_hostname", insertReq.hostName, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("an_userid", insertReq.userId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("ad_frmdate", insertReq.fromDate, OracleMappingType.Date, ParameterDirection.Input);
			parameters.Add("ad_todate", insertReq.toDate, OracleMappingType.Date, ParameterDirection.Input);
			parameters.Add("as_outresult", result, OracleMappingType.Varchar2, ParameterDirection.Output);

			parameters.BindByName = true;
			using var connection = _context.CreateConnection();
			await connection.QueryFirstOrDefaultAsync
				(procedureName, parameters, commandType: CommandType.StoredProcedure);

			_dto.insertRes.response = Convert.ToString(parameters.Get<string>("as_outresult"));

			return _dto.insertRes;
		}

		public async Task<PortalValidateResDto> PortalValidation(PortalValidateReqDto validateReq)
		{
			var branchId = string.Empty;
            var branchName = string.Empty;
			var mailFlag = string.Empty;
			var result = string.Empty;

			var procedureName = "PLP_PORTAL_VALIDATION";
			var parameters = new OracleDynamicParameters();
			parameters.Add("as_mac_address", validateReq.macId, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_hostname", validateReq.hostName, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_osinstalldt", validateReq.osInstallDate, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("as_param", validateReq.param, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("an_userid", validateReq.userId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("an_empPunchedBr", validateReq.empPunchedBr, OracleMappingType.NVarchar2, ParameterDirection.Input);
			parameters.Add("an_roleID", validateReq.roleId, OracleMappingType.Int32, ParameterDirection.Input);
			parameters.Add("an_outBranchID", branchId, OracleMappingType.Int32, ParameterDirection.Output);
			parameters.Add("an_outBranchName", branchName, OracleMappingType.NVarchar2, ParameterDirection.Output);
			parameters.Add("as_mailflag", mailFlag, OracleMappingType.NVarchar2, ParameterDirection.Output);
			parameters.Add("as_outresult", result, OracleMappingType.NVarchar2, ParameterDirection.Output);

			parameters.BindByName = true;
			using var connection = _context.CreateConnection();
			await connection.QueryFirstOrDefaultAsync
				(procedureName, parameters, commandType: CommandType.StoredProcedure);

			_dto.validateRes.branchId = parameters.Get<int>("an_outBranchID");
			_dto.validateRes.branchName = parameters.Get<string>("an_outBranchName");
			_dto.validateRes.mailFlag = parameters.Get<string>("as_mailflag");
			_dto.validateRes.result = parameters.Get<string>("as_outresult");

			return _dto.validateRes;
		}

        public async Task<dynamic> SessionValidation(string session)
        {
            using var connection = _context.CreateConnection();
            var query = "select count(*) count from login_session a where a.sessionid= '" + session + "'";
            var sessioncount = await connection.QuerySingleOrDefaultAsync(query);  
           // var response = await connection.QueryAsync<dynamic>(procedureName, parameters, commandType: CommandType.StoredProcedure);

			//_dto.sessionCount.count= sessioncount.ToString();
            return sessioncount;

			        }


    }
}
