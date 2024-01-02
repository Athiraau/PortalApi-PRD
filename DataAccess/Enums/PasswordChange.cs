using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enums
{
    public enum PasswordChange
    {
        ValidateMobileNo = 1,
        ValidateEmpMobile = 2,
        SaveOTP = 3,
        GetOTPLife = 4,
        ValidateEmployee = 5,
        VerifyOTP = 6
    }
}
