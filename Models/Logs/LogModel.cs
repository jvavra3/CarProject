using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarProject.Models.Logs
{
    public class LogModel
    {
        [Key]
        public int LogId { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Ip Address")]
        public string IpAddress { get; set; }

        public string Area { get; set; }
        
        [Display(Name = "Time Access")]
        public string TimeAccess { get; set; }


    }
}