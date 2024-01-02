using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class VerifyOTPReqDto
    {
        public int empId { get; set; } = 0;
        public string OTP { get; set; } = string.Empty;
    }
}
