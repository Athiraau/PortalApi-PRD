using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class EmpOTPReqDto
    {
        public int empId { get; set; } = 0;
        public int accId { get; set; } = 0;
        public string mobileNo { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
    }
}
