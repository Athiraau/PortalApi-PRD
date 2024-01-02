using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dto.Request
{
    public class PCDetailsInsertReqDto
    {        
        [Required]
        public int branchId { get; set; }
        [Required]
        public string hostName { get; set; }
        [Required]
        public string macAddress { get; set; }
        public string biosId { get; set; }
        public string cpuId { get; set; }        
        public string installDt { get; set; }
        public int userId { get; set; }        
        public string softDtl { get; set; }
    }
}
