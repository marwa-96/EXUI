using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class VM_adUsRead
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsFirstAdminRow { get; set; }
        public bool isCurrent { get; set; }

        
    }
}