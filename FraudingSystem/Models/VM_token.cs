using Ext_FraudingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Ext_FraudingSystem
{
    public class VM_token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public string isAdmin { get; set; }
        public string issued { get; set; }

        
        
    }
}