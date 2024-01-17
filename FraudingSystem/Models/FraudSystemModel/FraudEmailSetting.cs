using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Ext_FraudingSystem.Models
{
    public class FraudEmailSettings 
    {
        [Key]
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
       
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AdminsEmailId { get; set; }
        public bool? EnableSsl { get; set; }







    }
}