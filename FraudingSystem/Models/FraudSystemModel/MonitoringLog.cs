using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class MonitoringLog 
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        private DateTime _LogDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime LogDate
        {
            get
            {
                return _LogDate.Date;
            }
            set
            {
                _LogDate = value.Date;
            }
        }
        [DataType(DataType.Time)]
        public DateTime LogTime { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IPAddress { get; set; }
        public string ComputerName { get; set; }
        public string BrowserName { get; set; }
        public string MacAddress { get; set; }
        [NotMapped]
        public DateTime LogTimeString { get; set; }
    }
}
