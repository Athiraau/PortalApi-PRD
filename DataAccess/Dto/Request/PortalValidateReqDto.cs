using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Dto.Request
{
    public class PortalValidateReqDto
    {
        public int empPunchedBr { get; set; }
        public string macId { get; set; }
        public string hostName { get; set; }
        public string osInstallDate { get; set; }
        public string param { get; set; }
        public int userId { get; set; }        
        public int roleId { get; set; } 
    }
}
