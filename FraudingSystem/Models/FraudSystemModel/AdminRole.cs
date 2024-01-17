using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class AdminRole
    {
        [Key]
        public int Id { get; set; }
        public int AdminId { get; set; }
    }
}