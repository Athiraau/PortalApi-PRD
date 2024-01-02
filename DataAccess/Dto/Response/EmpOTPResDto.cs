using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Response
{
    public class EmpOTPResDto
    {
        public int status { get; set; } = 0;
        public int OTPLife { get; set; } = 0;
        public string message { get; set; } = string.Empty;
    }
}
