using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Dto.Request
{
    public class PortalInsertReqDto
    {
        public string optflag { get; set; }
        public int firmId { get; set; }
        public int branchId { get; set; }
        public string macAddress { get; set; }
        public string sessionId { get; set; }
        public string param { get; set; }
        public string hostName { get; set; }
        public int userId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}


                                         
                                              
