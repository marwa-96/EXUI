using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class FraudIsCustomer
    {
        [Key]
        public int Id { get; set; }
        public bool IsCustomer { get; set; }
    }
}