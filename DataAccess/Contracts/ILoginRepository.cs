using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface ILoginRepository
    {
        public Task<string> GetEmployeePassword(int empCode);
        public Task<MessageResDto> ChangePassword(ChangePasswordReqDto pwdReq, string oldPassword);
        public Task<MessageResDto> ChangePasswordProd(int userName, byte[] password, byte[] oldPassword);
    }
}
