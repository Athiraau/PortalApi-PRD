using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System.Data;

namespace DataAccess.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly DapperContext _context;
        private readonly DtoWrapper _dto;
        public LoginRepository(DapperContext context, DtoWrapper dto)
        {
            _context = context;
            _dto = dto;
        }

        public async Task<string> GetEmployeePassword(int empCode)
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("empCode", empCode, OracleMappingType.NVarchar2, ParameterDirection.Input);
            var query = "select t.password from employee_master t where t.emp_code=:empCode";

            using var connection = _context.CreateConnection();
            var password = await connection.QuerySingleOrDefaultAsync<string>(query, parameters);

            return password;
        }

        public async Task<MessageResDto> ChangePassword(ChangePasswordReqDto pwdReq, string oldPassword)
        {
            var result = string.Empty;

            var procedureName = "change_passwd_new_uat";
            var parameters = new OracleDynamicParameters();
            parameters.Add("user_nm", pwdReq.empCode, OracleMappingType.Int32, ParameterDirection.Input);
            parameters.Add("oldpass", oldPassword, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("newpass", pwdReq.password, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("msg", result, OracleMappingType.NVarchar2, ParameterDirection.Output);

            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            await connection.QueryFirstOrDefaultAsync
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            _dto.MessageRes.message = parameters.Get<string>("msg");
            return _dto.MessageRes;
        }

        public async Task<MessageResDto> ChangePasswordProd(int userName, byte[] password, byte[] oldPassword)
        {
            var result = string.Empty;

            var procedureName = "change_passwd_new";
            var parameters = new OracleDynamicParameters();
            parameters.Add("user_nm", userName, OracleMappingType.Int32, ParameterDirection.Input);
            parameters.Add("oldpass", password, OracleMappingType.Raw, ParameterDirection.Input);
            parameters.Add("newpass", oldPassword, OracleMappingType.Raw, ParameterDirection.Input);
            parameters.Add("msg", result, OracleMappingType.NVarchar2, ParameterDirection.Output);

            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            await connection.QueryFirstOrDefaultAsync
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            _dto.MessageRes.message = Convert.ToString(parameters.Get<int>("msg"));
            return _dto.MessageRes;
        }
    }
}
