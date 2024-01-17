using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class Vm_Auth
    {
        public string Message { get; set; }
        public bool IsAUth { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCustomer { get; set; }
        public List<int> Roles { get; set; }
    }
}