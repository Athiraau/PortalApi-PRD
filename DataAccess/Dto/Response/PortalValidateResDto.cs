using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Dto.Response
{
    public class PortalValidateResDto
    {
        public int branchId { get; set; }
        public string branchName { get; set; }
        public string mailFlag { get; set; }
        public string result { get; set; }
    }
}
