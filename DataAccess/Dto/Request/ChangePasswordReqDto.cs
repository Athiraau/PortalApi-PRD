using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class ChangePasswordReqDto
    {
        public int empCode { get; set; } = 0;
        public string joingDate { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
        public string password { get; set;} = string.Empty;
    }
}
