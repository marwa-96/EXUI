using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ext_FraudingSystem.Models
{
    public class FraudStatus
    {
        [Key]
        public int Id { get;set;}
        public string Name { get; set; }
    }
}