using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class UsersPrivlidge
    {
        [Key]
        public int Id { get;set; }
        public string UserId { get;set; }
        public bool IsAdmin { get; set; }
    }
}